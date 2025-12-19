using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using Reloaded.Hooks.Definitions.X64;
using Unscrambler.Derivation;
using Unscrambler.SelfTest.Binary;
using Unscrambler.SelfTest.Binary.Packet;
using Unscrambler.Unscramble;

namespace Unscrambler.SelfTest;

public unsafe class CaptureHookManager : IDisposable
{
	private const string GenericRxSignature = "E8 ?? ?? ?? ?? 4C 8B 4F 10 8B 47 1C 45";
	private const string OtherCreateTargetCaller = "E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 77 9A";
	private const string CreateTargetSignature = "3B 0D ?? ?? ?? ?? 74 0E";
	
	private delegate nuint RxPrototype(byte* data, byte* a2, nuint a3, nuint a4, nuint a5);
	private readonly Hook<RxPrototype> _zoneRxHook;

	private delegate byte OtherCreateTargetCallerPrototype(void* a1, void* a2, void* a3);
	private readonly Hook<OtherCreateTargetCallerPrototype> _otherCreateTargetCallerHook;

	[Function([FunctionAttribute.Register.rcx, FunctionAttribute.Register.rdi], FunctionAttribute.Register.rax, false)]
	private delegate byte CreateTargetPrototype(int entityId, nint packetPtr);
	private readonly Hook<CreateTargetPrototype> _createTargetHook;

	private readonly IPluginLog _log;
	private readonly PluginState _state;
	private readonly TestDataManager _testDataManager;
	
	private readonly IUnscrambler _unscrambler;
	private readonly IKeyGenerator _keyGenerator;
	private bool _obfuscationOverride;

	private bool _ignoreCreateTarget;
	private readonly Queue<QueuedPacket> _zoneRxIpcQueue;

	public CaptureHookManager(
		IPluginLog log,
		MultiSigScanner multiScanner,
		PluginState state,
		IGameInteropProvider hooks,
		TestDataManager testDataManager)
	{
		_log = log;
		_state = state;
		_testDataManager = testDataManager;
		
		_unscrambler = UnscramblerFactory.ForGameVersion(Plugin.GameVersion);
		_keyGenerator = KeyGeneratorFactory.ForGameVersion(Plugin.GameVersion);
		
		var dispatcher = _state.Dispatcher;
		if (dispatcher != null)
		{
			var gameRandom = dispatcher->GameRandom;
			var packetRandom = dispatcher->LastPacketRandom;
			_keyGenerator.Keys[0] = (byte)(dispatcher->Key0 - gameRandom - packetRandom);
			_keyGenerator.Keys[1] = (byte)(dispatcher->Key1 - gameRandom - packetRandom);
			_keyGenerator.Keys[2] = (byte)(dispatcher->Key2 - gameRandom - packetRandom);
			
			_state.GeneratedKey1 = (byte)(dispatcher->Key0 - gameRandom - packetRandom);
			_state.GeneratedKey2 = (byte)(dispatcher->Key1 - gameRandom - packetRandom);
			_state.GeneratedKey3 = (byte)(dispatcher->Key2 - gameRandom - packetRandom);
			
			_state.KeysFromDispatcher = true;
			_obfuscationOverride = dispatcher->Key0 >= gameRandom + packetRandom;
			_state.ObfuscationEnabled = _obfuscationOverride;

			_log.Debug($"[CaptureHooks] Obfuscation override is {_obfuscationOverride}");
			_log.Debug($"[CaptureHooks] keys {dispatcher->Key0}, {dispatcher->Key1}, {dispatcher->Key2}");
			_log.Debug($"[CaptureHooks] game random {dispatcher->GameRandom}, packet random {dispatcher->LastPacketRandom}");
		}
		else
		{
			_log.Debug("[CaptureHooks] Dispatcher was null, so not initializing keys");
		}
		
		_zoneRxIpcQueue = new Queue<QueuedPacket>();
		
		var rxPtrs = multiScanner.ScanText(GenericRxSignature, 3);
		_zoneRxHook = hooks.HookFromAddress<RxPrototype>(rxPtrs[2], ZoneRxDetour);
		
		var createTargetPtr = multiScanner.ScanText(CreateTargetSignature);
		_createTargetHook = hooks.HookFromAddress<CreateTargetPrototype>(createTargetPtr, CreateTargetDetour);
		
		var otherCreateTargetCallerPtr = multiScanner.ScanText(OtherCreateTargetCaller);
		_otherCreateTargetCallerHook = hooks.
			HookFromAddress<OtherCreateTargetCallerPrototype>(otherCreateTargetCallerPtr, OtherCreateTargetCallerDetour);

		Enable();
	}

	public void Enable()
	{
		_zoneRxHook?.Enable();
		_createTargetHook?.Enable();
		_otherCreateTargetCallerHook?.Enable();
	}
	
	public void Disable()
	{
		_zoneRxHook?.Disable();
		_createTargetHook?.Disable();
		_otherCreateTargetCallerHook?.Disable();
		_zoneRxIpcQueue.Clear();
	}
	
	public void Dispose()
	{
		Disable();
		_zoneRxHook?.Dispose();
		_createTargetHook?.Dispose();
		_otherCreateTargetCallerHook?.Dispose();
	}
	
	private byte OtherCreateTargetCallerDetour(void* a1, void* a2, void* a3)
	{
		_ignoreCreateTarget = true;
		_log.Verbose($"[OtherCreateTargetCaller]: ignoring next CreateTarget");
		return _otherCreateTargetCallerHook.Original(a1, a2, a3);
	}
	
	private byte CreateTargetDetour(int entityId, nint packetPtr)
	{
		try
		{
			if (_ignoreCreateTarget)
			{
				_ignoreCreateTarget = false;
				return _createTargetHook.Original(entityId, packetPtr);
			}

			if (_zoneRxIpcQueue.Count == 0)
			{
				// SendNotification($"[CreateTarget]: Please report this problem: no packets in queue");
				return _createTargetHook.Original(entityId, packetPtr);
			}

			QueuedPacket? queuedPacket = null;
			var opcode = OpcodeUtility.GetOpcodeFromPacketAtIpcStart(new Span<byte>((void*)packetPtr, 4));
			
			if (opcode == Plugin.Constants.InitZoneOpcode)
			{
				_log.Verbose($"[CreateTarget]: {opcode:X} InitZone :)");
				return _createTargetHook.Original(entityId, packetPtr);
			}
			
			if (!_state.OpcodesNeeded.Contains(opcode))
			{
				return _createTargetHook.Original(entityId, packetPtr);
			} 
			
			do
			{
				var packet = _zoneRxIpcQueue.Dequeue();
				if (packet.Opcode == opcode && packet.Source == entityId)
					queuedPacket = packet;
			} while (queuedPacket == null && _zoneRxIpcQueue.Count > 0);

			if (queuedPacket == null)
			{
				_log.Verbose($"[CreateTarget]: entity {entityId} opcode {opcode:X} not found in queue");
				return _createTargetHook.Original(entityId, packetPtr);
			}

			_log.Verbose($"[CreateTarget]: entity {entityId} meta source {queuedPacket.Source} size {queuedPacket.DataSize} opcode {queuedPacket.Opcode:X}");

			// Set the packet's data
			var data = new Span<byte>((byte*)packetPtr, queuedPacket.DataSize);
			queuedPacket.GameData = data.ToArray();
			queuedPacket.GameDataHash = HashPacket(queuedPacket.GameData);
			
			var scrambledHash = HashPacket(queuedPacket.ScrambledData);
			if (scrambledHash == queuedPacket.GameDataHash)
			{
				if (queuedPacket.Opcode != Plugin.Constants.ObfuscatedOpcodes["ActorControl"])
					_log.Error($"A tracked packet {queuedPacket.Opcode} ({queuedPacket.Opcode:X}) had no changes after the game's unscramble process!");
			}
			
			if (queuedPacket.GameDataHash == queuedPacket.UnscramblerDataHash
			    && queuedPacket.GameDataHash == queuedPacket.UnscramblerDataHash2)
			{
				_state.MarkOpcode(queuedPacket.Opcode, true);
				_testDataManager.Save(opcode, queuedPacket.ScrambledData, queuedPacket.UnscramblerData);
			}
			else
			{
				_state.MarkOpcode(queuedPacket.Opcode, false);
				_log.Verbose($"opcode {queuedPacket.Opcode:X} unscramble failed");

				var indices = new List<int>();
				for (int i = 0; i < queuedPacket.GameData.Length; i++)
				{
					if (queuedPacket.GameData[i] != queuedPacket.UnscramblerData[i])
					{
						indices.Add(i);
					}
				}

				var unscrambleStr = "";
				var gameStr = "";
				for (int i = 0; i < queuedPacket.DataSize; i++)
				{
					if (!indices.Contains(i))
					{
						unscrambleStr += "__ ";
						gameStr += "__ ";
					}
					else
					{
						unscrambleStr += $"{queuedPacket.UnscramblerData[i]:X2} ";
						gameStr += $"{queuedPacket.GameData[i]:X2} ";
					}
				}

				_log.Verbose($"bad indices ({indices.Count}): {string.Join(", ", indices)}");
				_log.Verbose($"unscramble: {unscrambleStr}");
				_log.Verbose($"      game: {gameStr}");
			}
		}
		catch (Exception e)
		{
			_log.Error(e, "[CreateTarget]: Error");
		}
		
		return _createTargetHook.Original(entityId, packetPtr);
	}

    private nuint ZoneRxDetour(byte* data, byte* a2, nuint a3, nuint a4, nuint a5)
    {
	    // _log.Verbose($"ZoneRxDetour: {(long)data:X} {(long)a2:X} {a3:X} {a4:X} {a5:X}");
	    var ret = _zoneRxHook.Original(data, a2, a3, a4, a5);

	    var packetOffset = *(uint*)(data + 28);
	    if (packetOffset != 0) return ret;
	    
        PacketsFromFrame(Protocol.Zone, Direction.Rx, (byte*) *(nint*)(data + 16));

        return ret;
    }
    
    private void PacketsFromFrame(Protocol proto, Direction direction, byte* framePtr)
    {
        try
        {
            PacketsFromFrame2(proto, direction, framePtr);
        }
        catch (Exception e)
        {
            _log.Error(e, "[PacketsFromFrame] Error!!!!!!!!!!!!!!!!!!");
        }
    }
    
    private void PacketsFromFrame2(Protocol proto, Direction direction, byte* framePtr)
    {
        if ((nuint)framePtr == 0)
        {
            _log.Error("null ptr");
            return;
        }
        
        var headerSize = Unsafe.SizeOf<FrameHeader>();
        var headerSpan = new Span<byte>(framePtr, headerSize);
        var header = headerSpan.Cast<byte, FrameHeader>();
        var span = new Span<byte>(framePtr, (int)header.TotalSize);
        var data = span.Slice(headerSize, (int)header.TotalSize - headerSize);
        
        _log.Verbose($"[{(nuint)framePtr:X}] [{proto}{direction}] proto {header.Protocol} unk {header.Unknown}, {header.Count} pkts size {header.TotalSize} usize {header.DecompressedLength}{DispatcherDebugSuffix()}");
        
        // Compression
        if (header.Compression != CompressionType.None)
        {
            return;
        }

        var hasIpc = false;
        var hasNonIpc = false;
        
        var offset = 0;
        for (int i = 0; i < header.Count; i++)
        {
	        var pktHdrSize = Unsafe.SizeOf<PacketElementHeader>();
            var pktHdrSlice = data.Slice(offset, pktHdrSize);
            var pktHdr = pktHdrSlice.Cast<byte, PacketElementHeader>();
            var pktData = data.Slice(offset + pktHdrSize, (int)pktHdr.Size - pktHdrSize);
            hasIpc |= pktHdr.Type is PacketType.Ipc;
            hasNonIpc |= pktHdr.Type is not PacketType.Ipc;

            var queuedPacket = new QueuedPacket
            {
	            Source = pktHdr.SrcEntity,
	            DataSize = pktData.Length,
	            Header = pktHdrSlice.ToArray()
            };

            var opcode = OpcodeUtility.GetOpcodeFromPacketAtIpcStart(pktData);
            queuedPacket.Opcode = opcode;
            
            if (opcode == Plugin.Constants.UnknownObfuscationInitOpcode)
            {
	            try
	            {
		            _testDataManager.SaveUnknownInit(pktData);
		            _log.Verbose($"generating keys");
		            _keyGenerator.GenerateFromUnknownInitializer(pktData);
		            _state.ObfuscationEnabled = _keyGenerator.ObfuscationEnabled;
		            _state.GeneratedKey1 = _keyGenerator.Keys[0];
		            _state.GeneratedKey2 = _keyGenerator.Keys[1];
		            _state.GeneratedKey3 = _keyGenerator.Keys[2];
		            _state.KeysFromDispatcher = false;
		            _obfuscationOverride = false;
	            }
	            catch (Exception e)
	            {
		            _log.Error(e, "Failed to generate keys");
	            }
            }

            if (proto == Protocol.Zone && 
                direction == Direction.Rx &&
                pktHdr.Type == PacketType.Ipc && 
                _state.OpcodesNeeded.Contains(opcode))
            {
	            // Note that if you do want to unscramble using the game's keys, you need to do it later, in CreateTarget
	            // if you're not, you need to do it here.
	            // If you don't, you may update keys before a packet prior to the InitZone could be unscrambled
	            queuedPacket.ScrambledData = pktData.ToArray();
	            queuedPacket.UnscramblerData = pktData.ToArray();
	            queuedPacket.UnscramblerData2 = pktData.ToArray();
	            if (_keyGenerator.ObfuscationEnabled || _obfuscationOverride)
	            {
		            _log.Verbose($"unscrambling {opcode:X}");
		            _state.OpcodeBasedKey = _keyGenerator.GetOpcodeBasedKey(opcode);
		            _unscrambler.Unscramble(
			            queuedPacket.UnscramblerData, 
			            _keyGenerator.Keys[0],
			            _keyGenerator.Keys[1],
			            _keyGenerator.Keys[2],
			            _state.OpcodeBasedKey);
		            
		            _unscrambler.Unscramble(
			            queuedPacket.UnscramblerData2,
			            _keyGenerator.Keys[0],
			            _keyGenerator.Keys[1],
			            _keyGenerator.Keys[2],
			            _state.OpcodeKeyTable);
	            }
	            queuedPacket.UnscramblerDataHash = HashPacket(queuedPacket.UnscramblerData);
	            queuedPacket.UnscramblerDataHash2 = HashPacket(queuedPacket.UnscramblerData2);
	            
	            _zoneRxIpcQueue.Enqueue(queuedPacket);
            }
            
            var opcodeInfo = "";
            if (pktHdr.Type == PacketType.Ipc)
	            opcodeInfo = $"opcode {opcode} ({opcode:X}), ";
            _log.Verbose($"packet: type {pktHdr.Type}, {opcodeInfo}{pktHdr.Size} bytes, {pktHdr.SrcEntity} -> {pktHdr.DstEntity}{DispatcherDebugSuffix()}");
            offset += (int)pktHdr.Size;
        }
    }

    private string DispatcherDebugSuffix()
    {
	    return $"| {_state.ObfuscationEnabled} {_state.GeneratedKey1} {_state.GeneratedKey2} {_state.GeneratedKey3} | {_state.OpcodeBasedKey}";
    }

    private string HashPacket(Span<byte> data)
    {
	    var bytes = SHA256.HashData(data);
	    return Convert.ToHexString(bytes);
    }
}
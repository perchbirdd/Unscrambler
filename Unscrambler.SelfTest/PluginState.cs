using System.Collections.Generic;
using System.Linq;

namespace Unscrambler.SelfTest;

public unsafe class PluginState
{
    public PacketDispatcher* Dispatcher { get; set; }

    public bool KeysFromDispatcher { get; set; }
    public bool ObfuscationEnabled { get; set; }
    public byte GeneratedKey1 { get; set; }
    public byte GeneratedKey2 { get; set; }
    public byte GeneratedKey3 { get; set; }
    
    public HashSet<int> OpcodesNeeded { get; set; }
    
    public Dictionary<int, int> OpcodeSuccesses { get; init; }
    public Dictionary<int, int> OpcodeFailures { get; init; }

    public PluginState()
    {
        OpcodesNeeded = [];
        OpcodeSuccesses = [];
        OpcodeFailures = [];
        
        OpcodesNeeded = Plugin.Constants.ObfuscatedOpcodes.Select(op => op.Value).ToHashSet();
    }

    public void MarkOpcode(ushort opcode, bool success)
    {
        if (success)
        {
            var count = OpcodeSuccesses.GetValueOrDefault(opcode, 0);
            OpcodeSuccesses[opcode] = count + 1;
        }
        else
        {
            var count = OpcodeFailures.GetValueOrDefault(opcode, 0);
            OpcodeFailures[opcode] = count + 1;
        }
    }
}
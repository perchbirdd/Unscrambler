using System.Runtime.CompilerServices;
using Unscrambler.Constants;

namespace Unscrambler.Unscramble.Versions;

public unsafe class Unscrambler73 : IUnscrambler
{
    private VersionConstants _constants;

    public void Initialize(VersionConstants constants)
    {
        _constants = constants;
    }
    
    public void Unscramble(Span<byte> input, byte key0, byte key1, byte key2)
    {
        // 7.3 requires the opcode-based key
        throw new NotImplementedException("The opcode-based key is required for 7.3 Unscrambler implementations.");
    }

    public void Unscramble(Span<byte> input, byte key0, byte key1, byte key2, int opcodeBasedKey)
    {
        // Ditch - something is uninitialized, and there's no way to know if it's on purpose or not
        if (key0 == 0 && key1 == 0 && key2 == 0 && opcodeBasedKey == 0) return;
        
        Span<uint> keys = stackalloc uint[3];
        keys[0] = key0;
        keys[1] = key1;
        keys[2] = key2;
        
        var opcode = OpcodeUtility.GetOpcodeFromPacketAtIpcStart(input);
        
        var baseKey = (byte) keys[opcode % 3];
        var data = (byte*)Unsafe.AsPointer(ref input[0]);
        
        switch (true)
        {
            // PlayerSpawn
            case true when opcode == _constants.ObfuscatedOpcodes["PlayerSpawn"]:
            {
                *(ulong*)(data + 24) -= baseKey;  // Content ID
                *(ushort*)(data + 36) -= baseKey; // Home world 
                *(ushort*)(data + 38) -= baseKey; // Current world
                
                // Name
                const int nameOffset = 610;
                for (int i = 0; i < 32; i++)
                {
                    data[nameOffset + i] -= baseKey;
                }
                
                // Equipment
                const int equipOffset = 556;
                var intKeyToUse = baseKey + opcodeBasedKey;
                for (int i = 0; i < 10; i++)
                {
                    var offset = equipOffset + i * 4;
                    *(int*)(data + offset) ^= intKeyToUse;
                }
                break;
            }
            // NpcSpawn
            case true when opcode == _constants.ObfuscatedOpcodes["NpcSpawn"]:
            {
                UnscrambleNpcSpawn(data, baseKey, (uint) opcodeBasedKey);
                break;
            }
            // NpcSpawn2
            case true when opcode == _constants.ObfuscatedOpcodes["NpcSpawn2"]:
            {
                UnscrambleNpcSpawn(data, baseKey, (uint) opcodeBasedKey);
                break;
            }
            // ActionEffect
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect01"]:
            {
                UnscrambleActionEffect(data, 1, baseKey, opcodeBasedKey);
                break;
            }
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect08"]:
            {
                UnscrambleActionEffect(data, 8, baseKey, opcodeBasedKey);
                break;
            }
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect16"]:
            {
                UnscrambleActionEffect(data, 16, baseKey, opcodeBasedKey);
                break;
            }
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect24"]:
            {
                UnscrambleActionEffect(data, 24, baseKey, opcodeBasedKey);
                break;
            }
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect32"]:
            {
                UnscrambleActionEffect(data, 32, baseKey, opcodeBasedKey);
                break;
            }
            // StatusEffectList
            case true when opcode == _constants.ObfuscatedOpcodes["StatusEffectList"]:
            {
                UnscrambleStatusEffectList(data, baseKey, 36);
                break;
            }
            case true when opcode == _constants.ObfuscatedOpcodes["StatusEffectList3"]:
            {
                UnscrambleStatusEffectList(data, baseKey, 16);
                break;
            }

            // Examine
            case true when opcode == _constants.ObfuscatedOpcodes["Examine"]:
            {
                data[18] -= baseKey;
                data[19] -= baseKey; 
                *(ushort*)(data + 66) -= baseKey;
                *(ulong*)(data + 72) -= baseKey;
                
                const int opOffset1 = 656;
                for (int i = 0; i < 32; i++)
                {
                    data[opOffset1 + i] -= baseKey;
                }
                
                const int opOffset2 = 688;
                for (int i = 0; i < 32; i++)
                {
                    data[opOffset2 + i] -= baseKey;
                }
                break;
            }
            // UpdateGearset
            case true when opcode == _constants.ObfuscatedOpcodes["UpdateGearset"]:
            {
                const int opOffset = 36;
                var intKeyToUse = baseKey + opcodeBasedKey;
                for (int i = 0; i < 10; i++)
                {
                    var offset = opOffset + i * 4;
                    *(int*)(data + offset) ^= intKeyToUse;
                }
                
                break;
            }
            // UpdateParty
            case true when opcode == _constants.ObfuscatedOpcodes["UpdateParty"]:
            {
                for (int i = 0; i < 8; i++)
                {
                    var offset = 456 * i;
                    *(ulong*)(data + 64 + offset) -= baseKey;
                    *(uint*)(data + 72 + offset) -= baseKey;
                    *(uint*)(data + 76 + offset) -= baseKey;
                    *(uint*)(data + 80 + offset) -= baseKey;
                    *(ushort*)(data + 96 + offset) -= baseKey;
                    data[101 + offset] -= baseKey;
                    data[103 + offset] -= baseKey;
                    data[105 + offset] -= baseKey;
                }
                break;
            }
            // ActorControl
            case true when opcode == _constants.ObfuscatedOpcodes["ActorControl"]:
            {
                // If ActorControl is TargetIcon
                if (*(ushort*)(data + 16) == 34)
                    *(uint*)(data + 20) -= baseKey;
                break;
            }
            // ActorCast
            case true when opcode == _constants.ObfuscatedOpcodes["ActorCast"]:
            {
                *(uint*)(data + 20) -= baseKey;
                break;   
            }
            // --- Unknown opcodes ---
            // - ActionEffect2?
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect02"]:
            {
                UnscrambleActionEffect(data, 2, baseKey, opcodeBasedKey);
                break;
            }
            // - ActionEffect4?
            case true when opcode == _constants.ObfuscatedOpcodes["ActionEffect04"]:
            {
                UnscrambleActionEffect(data, 4, baseKey, opcodeBasedKey);
                break;
            }
            // Actually unknown
            case true when opcode == _constants.ObfuscatedOpcodes["UnknownEffect01"]:
            {
                *(uint *)(data + 28) -= baseKey;
                
                var opOffset = 66;
                var shortKey = (short) (baseKey + opcodeBasedKey);
                var targetCount = 1;
                for (int i = 0; i < 8 * targetCount; i++)
                {
                    var offset = opOffset + i * 8;
                    *(short *)(data + offset) ^= shortKey;    
                }
                break;
            }
            // Actually unknown
            case true when opcode == _constants.ObfuscatedOpcodes["UnknownEffect16"]:
            {
                *(uint *)(data + 28) -= baseKey;
                
                var opOffset = 58;
                var shortKey = (short) (baseKey + opcodeBasedKey);
                var targetCount = 16;
                for (int i = 0; i < 8 * targetCount; i++)
                {
                    var offset = opOffset + i * 8;
                    *(short *)(data + offset) ^= shortKey;    
                }
                break;
            }
        }
    }

    private void UnscrambleStatusEffectList(byte* data, byte baseKey, int opOffset)
    {
        for (int i = 0; i < 30; i++)
        {
            var offset = opOffset + i * 12;
            *(short *)(data + offset) -= baseKey;
        }
    }

    private void UnscrambleNpcSpawn(byte* data, byte baseKey, uint weirdConst)
    {
        *(uint*)(data +  80) -= baseKey;
        *(uint*)(data +  84) -= baseKey;
        *(uint*)(data +  88) -= baseKey;
        *(uint*)(data +  96) -= baseKey;
        *(uint*)(data + 100) -= baseKey;
        *(uint*)(data + 108) ^= weirdConst;
                
        var opOffset = 168;
                
        for (int i = 0; i < 30; i++)
        {
            var offset = opOffset + i * 12;
            *(ushort*)(data + offset) -= baseKey;
        }
    }

    private void UnscrambleActionEffect(byte* data, int targetCount, byte baseKey, int baseKeyMod)
    {
        *(uint *)(data + 24) -= baseKey; // Action ID
                
        var damageValueOffset = 64;
        var newShortKey = (short) (baseKey + baseKeyMod);
        for (int i = 0; i < 8 * targetCount; i++)
        {
            var offset = damageValueOffset + i * 8;
            *(short *)(data + offset) ^= newShortKey;    
        }
    }
}
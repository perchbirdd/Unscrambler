
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For735h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.10.13.0000.0000",
            TableOffsets = [0x2188E70, 0x219D690, 0x21A9110],
            TableSizes = [21000 * 4, 11934 * 4, 11616 * 4],
            TableRadixes = [100, 117, 96],
            TableMax = [210, 102, 121],
            MidTableOffset = 0x2188B60,
            MidTableSize = 98 * 8,
            DayTableOffset = 0x21B4690,
            DayTableSize = 15 * 4,
            OpcodeKeyTableOffset = 0x21B46D0,
            OpcodeKeyTableSize = 70 * 4,
            ObfuscationEnabledMode = 96,
            InitZoneOpcode = 0x3A3,
            UnknownObfuscationInitOpcode = 0x2AB,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0xE3 },
                { "NpcSpawn", 0x2EA },
                { "NpcSpawn2", 0x181 },
                { "ActionEffect01", 0xF2 },
                { "ActionEffect08", 0x140 },
                { "ActionEffect16", 0x273 },
                { "ActionEffect24", 0x175 },
                { "ActionEffect32", 0x377 },
                { "StatusEffectList", 0x3D4 },
                { "StatusEffectList3", 0x38D },
                { "Examine", 0x241 },
                { "UpdateGearset", 0x3B8 },
                { "UpdateParty", 0x37A },
                { "ActorControl", 0xA1 },
                { "ActorCast", 0x355 },
                { "UnknownEffect01", 0x3E7 },
                { "UnknownEffect16", 0x383 },
                { "ActionEffect02", 0x86 },
                { "ActionEffect04", 0x112 }
            }
        };
    }
}
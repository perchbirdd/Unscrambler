namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For751h1()
    {
        return new VersionConstants
        {
            GameVersion = "2026.06.10.0000.0000",
            TableOffsets = [0x22DFDC0, 0x22E6F70, 0x22FDAB0],
            TableSizes = [7276 * 4, 23246 * 4, 21243 * 4],
            TableRadixes = [107, 118, 97],
            TableMax = [68, 197, 219],
            MidTableOffset = 0x22DF6F0,
            MidTableSize = 218 * 8,
            DayTableOffset = 0x23126A0,
            DayTableSize = 30 * 4,
            OpcodeKeyTableSize = 103 * 4,
            OpcodeKeyTableOffset = 0x2312720,
            ObfuscationEnabledMode = 240,
            InitZoneOpcode = 0x8B,
            UnknownObfuscationInitOpcode = 0x1D0,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x3B4 },
                { "NpcSpawn", 0x113 },
                { "NpcSpawn2", 0xB8 },

                { "ActionEffect01", 0x1D9 },
                { "ActionEffect08", 0x141 },
                { "ActionEffect16", 0x191 },
                { "ActionEffect24", 0x231 },
                { "ActionEffect32", 0x38B },

                { "StatusEffectList", 0x12B },
                { "StatusEffectList3", 0xEF },

                { "Examine", 0x1BB },
                { "UpdateGearset", 0x1C4 },
                { "UpdateParty", 0x34C },
                { "ActorControl", 0x27F },
                { "ActorCast", 0x2F8 },

                { "UnknownEffect01", 0x213 },
                { "UnknownEffect16", 0x234 },
                { "ActionEffect02", 0x2F9 },
                { "ActionEffect04", 0x3A3 }
            },
        };
    }
}
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For756()
    {
        return new VersionConstants
        {
            GameVersion = "2026.09.01.0000.0000",
            TableOffsets = [0x22ECFE0, 0x2302BB0, 0x2307560],
            TableSizes = [22260 * 4, 4715 * 4, 10912 * 4],
            TableRadixes = [105, 115, 124],
            TableMax = [212, 41, 88],
            MidTableOffset = 0x22EC820,
            MidTableSize = 248 * 8,
            DayTableOffset = 0x2311FE0,
            DayTableSize = 21 * 4,
            OpcodeKeyTableSize = 193 * 4,
            OpcodeKeyTableOffset = 0x2312040,
            ObfuscationEnabledMode = 12,
            InitZoneOpcode = 0x3A1,
            UnknownObfuscationInitOpcode = 0x66,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x3B2 },
                { "NpcSpawn", 0x1C4 },
                { "NpcSpawn2", 0x26A },

                { "ActionEffect01", 0x2EC },
                { "ActionEffect08", 0xFD },
                { "ActionEffect16", 0x357 },
                { "ActionEffect24", 0xB4 },
                { "ActionEffect32", 0x14E },

                { "StatusEffectList", 0x248 },
                { "StatusEffectList3", 0x20D },

                { "Examine", 0x69 },
                { "UpdateGearset", 0x374 },
                { "UpdateParty", 0x1DF },
                { "ActorControl", 0x38C },
                { "ActorCast", 0x10A },

                { "UnknownEffect01", 0x1DB },
                { "UnknownEffect16", 0x34C },
                { "ActionEffect02", 0x356 },
                { "ActionEffect04", 0x371 }
            },
        };
    }
}

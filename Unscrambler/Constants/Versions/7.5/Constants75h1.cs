namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For75h1()
    {
        return new VersionConstants
        {
            GameVersion = "2026.05.01.0000.0000",
            TableOffsets = [0x22E29E0, 0x22EAE00, 0x22F2060],
            TableSizes = [8455 * 4, 7320 * 4, 11220 * 4],
            TableRadixes = [95, 122, 110],
            TableMax = [89, 60, 102],
            MidTableOffset = 0x22E2290,
            MidTableSize = 233 * 8,
            DayTableOffset = 0x22FCFB0,
            DayTableSize = 36 * 4,
            OpcodeKeyTableSize = 35 * 4,
            OpcodeKeyTableOffset = 0x22FD040,
            ObfuscationEnabledMode = 93,
            InitZoneOpcode = 0x237,
            UnknownObfuscationInitOpcode = 0x3C2,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x2AA },
                { "NpcSpawn", 0xF6 },
                { "NpcSpawn2", 0xA1 },

                { "ActionEffect01", 0x1FF },
                { "ActionEffect08", 0x283 },
                { "ActionEffect16", 0x309 },
                { "ActionEffect24", 0x115 },
                { "ActionEffect32", 0x29A },

                { "StatusEffectList", 0x65 },
                { "StatusEffectList3", 0x249 },

                { "Examine", 0x2C4 },
                { "UpdateGearset", 0xB6 },
                { "UpdateParty", 0x371 },
                { "ActorControl", 0x343 },
                { "ActorCast", 0x25C },

                { "UnknownEffect01", 0x3E2 },
                { "UnknownEffect16", 0x16A },
                { "ActionEffect02", 0x207 },
                { "ActionEffect04", 0xA8 }
            },
        };
    }
}
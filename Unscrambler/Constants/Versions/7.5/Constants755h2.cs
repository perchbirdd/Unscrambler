namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For755h2()
    {
        return new VersionConstants
        {
            GameVersion = "2026.08.11.0000.0000",
            TableOffsets = [0x22E2A30, 0x22EF0F0, 0x2302330],
            TableSizes = [12720 * 4, 19600 * 4, 22599 * 4],
            TableRadixes = [106, 98, 93],
            TableMax = [120, 200, 243],
            MidTableOffset = 0x22E24E0,
            MidTableSize = 169 * 8,
            DayTableOffset = 0x2318450,
            DayTableSize = 27 * 4,
            OpcodeKeyTableSize = 77 * 4,
            OpcodeKeyTableOffset = 0x23184C0,
            ObfuscationEnabledMode = 138,
            InitZoneOpcode = 0x161,
            UnknownObfuscationInitOpcode = 0x1A4,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x32D },
                { "NpcSpawn", 0xE9 },
                { "NpcSpawn2", 0x21C },

                { "ActionEffect01", 0x371 },
                { "ActionEffect08", 0x3C8 },
                { "ActionEffect16", 0x1AF },
                { "ActionEffect24", 0x35A },
                { "ActionEffect32", 0x3D5 },

                { "StatusEffectList", 0x2EC },
                { "StatusEffectList3", 0x263 },

                { "Examine", 0x97 },
                { "UpdateGearset", 0x173 },
                { "UpdateParty", 0x3E4 },
                { "ActorControl", 0x96 },
                { "ActorCast", 0x136 },

                { "UnknownEffect01", 0x33C },
                { "UnknownEffect16", 0x1D8 },
                { "ActionEffect02", 0x2F4 },
                { "ActionEffect04", 0x3AD }
            },
        };
    }
}
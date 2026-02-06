namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For741h1()
    {
        return new VersionConstants
        {
            GameVersion = "2026.01.30.0000.0000",
            TableOffsets = [0x21F18E0, 0x2205FE0, 0x2209EE0],
            TableSizes = [20928 * 4, 4032 * 4, 13566 * 4],
            TableRadixes = [96, 96, 133],
            TableMax = [218, 42, 102],
            MidTableOffset = 0x21F1490,
            MidTableSize = 137 * 8,
            DayTableOffset = 0x22172E0,
            DayTableSize = 31 * 4,
            OpcodeKeyTableSize = 209 * 4,
            OpcodeKeyTableOffset = 0x2217360,
            ObfuscationEnabledMode = 159,
            InitZoneOpcode = 0x2C0,
            UnknownObfuscationInitOpcode = 0xF8,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x221 },
                { "NpcSpawn", 0x197 },
                { "NpcSpawn2", 0x120 },

                { "ActionEffect01", 0x1CC },
                { "ActionEffect08", 0x3DF },
                { "ActionEffect16", 0x7B },
                { "ActionEffect24", 0x2FE },
                { "ActionEffect32", 0x265 },

                { "StatusEffectList", 0x366 },
                { "StatusEffectList3", 0x24E },

                { "Examine", 0x134 },
                { "UpdateGearset", 0x3CB },
                { "UpdateParty", 0x188 },
                { "ActorControl", 0x1EC },
                { "ActorCast", 0x3AD },

                { "UnknownEffect01", 0x272 },
                { "UnknownEffect16", 0x29E },
                { "ActionEffect02", 0x3DD },
                { "ActionEffect04", 0x3DB }
            },
        };
    }
}

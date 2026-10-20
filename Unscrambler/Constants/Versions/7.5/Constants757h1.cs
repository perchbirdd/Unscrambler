namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For757h1()
    {
        return new VersionConstants
        {
            GameVersion = "2026.10.16.0000.0000",
            TableOffsets = [0x22EC5A0, 0x2300820, 0x2311A30],
            TableSizes = [20640 * 4, 17538 * 4, 25334 * 4],
            TableRadixes = [96, 111, 106],
            TableMax = [215, 158, 239],
            MidTableOffset = 0x22EBF50,
            MidTableSize = 202 * 8,
            DayTableOffset = 0x232A610,
            DayTableSize = 60 * 4,
            OpcodeKeyTableSize = 233 * 4,
            OpcodeKeyTableOffset = 0x232A700,
            ObfuscationEnabledMode = 47,
            InitZoneOpcode = 0x9C,
            UnknownObfuscationInitOpcode = 0x34E,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x17C },
                { "NpcSpawn", 0x17D },
                { "NpcSpawn2", 0x1AB },

                { "ActionEffect01", 0x363 },
                { "ActionEffect08", 0x262 },
                { "ActionEffect16", 0x299 },
                { "ActionEffect24", 0x245 },
                { "ActionEffect32", 0x385 },

                { "StatusEffectList", 0xD4 },
                { "StatusEffectList3", 0x28E },

                { "Examine", 0x19E },
                { "UpdateGearset", 0x330 },
                { "UpdateParty", 0x2E7 },
                { "ActorControl", 0x229 },
                { "ActorCast", 0x7F },

                { "UnknownEffect01", 0x26F },
                { "UnknownEffect16", 0x368 },
                { "ActionEffect02", 0x362 },
                { "ActionEffect04", 0x2E9 }
            },
        };
    }
}

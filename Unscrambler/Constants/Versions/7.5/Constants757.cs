namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For757()
    {
        return new VersionConstants
        {
            GameVersion = "2026.10.06.0000.0000",
            TableOffsets = [0x22EB640, 0x22FDC40, 0x2302B10],
            TableSizes = [18816 * 4, 5043 * 4, 21816 * 4],
            TableRadixes = [84, 123, 108],
            TableMax = [224, 41, 202],
            MidTableOffset = 0x22EB370,
            MidTableSize = 89 * 8,
            DayTableOffset = 0x2317FF0,
            DayTableSize = 15 * 4,
            OpcodeKeyTableSize = 83 * 4,
            OpcodeKeyTableOffset = 0x2318030,
            ObfuscationEnabledMode = 254,
            InitZoneOpcode = 0x2CC,
            UnknownObfuscationInitOpcode = 0x2FE,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x265 },
                { "NpcSpawn", 0x114 },
                { "NpcSpawn2", 0x2F2 },

                { "ActionEffect01", 0xBD },
                { "ActionEffect08", 0x13F },
                { "ActionEffect16", 0x390 },
                { "ActionEffect24", 0x9E },
                { "ActionEffect32", 0x276 },

                { "StatusEffectList", 0x2CE },
                { "StatusEffectList3", 0x97 },

                { "Examine", 0x19C },
                { "UpdateGearset", 0x3D5 },
                { "UpdateParty", 0x16A },
                { "ActorControl", 0x198 },
                { "ActorCast", 0x301 },

                { "UnknownEffect01", 0x2D9 },
                { "UnknownEffect16", 0x127 },
                { "ActionEffect02", 0x353 },
                { "ActionEffect04", 0x37C }
            },
        };
    }
}

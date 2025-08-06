namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For721()
    {
        return new VersionConstants
        {
            GameVersion = "2025.04.16.0000.0000",
            TableOffsets = [0x216C700, 0x217EDB0, 0x218C4C0],
            TableSizes = [18860 * 4, 13764 * 4, 31000 * 4],
            TableRadixes = [109, 111, 125],
            TableMax = [173, 124, 248],
            MidTableOffset = 0x216C310,
            MidTableSize = 126 * 8,
            DayTableOffset = 0x21AA920,
            DayTableSize = 14 * 4,
            ObfuscationEnabledMode = 41,
            InitZoneOpcode = 0x316,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x374 },
                { "NpcSpawn", 0x25E },
                { "NpcSpawn2", 0x3CB },

                { "ActionEffect01", 0xF5 },
                { "ActionEffect08", 0x32D },
                { "ActionEffect16", 0x10F },
                { "ActionEffect24", 0x188 },
                { "ActionEffect32", 0x13E },

                { "StatusEffectList", 0x27B },
                { "StatusEffectList3", 0x1A6 },

                { "Examine", 0xD0 },
                { "UpdateGearset", 0x34B },
                { "UpdateParty", 0x2BA },
                { "ActorControl", 0x9C },
                { "ActorCast", 0x1D4 },

                { "UnknownEffect01", 0x256 },
                { "UnknownEffect16", 0x148 },
                { "ActionEffect02", 0x6A },
                { "ActionEffect04", 0x1BF }
            },
        };
    }
}
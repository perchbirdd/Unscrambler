namespace Unscrambler.Constants.Versions;

public partial class GameConstants
{
    public static VersionConstants For72()
    {
        return new VersionConstants
        {
            GameVersion = "2025.03.18.0000.0000",
            TableOffsets = [0x2168E40, 0x2178DD0, 0x21875D0],
            TableSizes = [16356 * 4, 14848 * 4, 18476 * 4],
            TableRadixes = [116, 101, 124],
            TableMax = [141, 147, 149],
            MidTableOffset = 0x2168850,
            MidTableSize = 1516 + 4,
            DayTableOffset = 0x2199680,
            DayTableSize = (0x3D + 1) * 4,
            ObfuscationEnabledMode = 5,
            InitZoneOpcode = 0x2BB,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x331 },
                { "NpcSpawn", 0x13F },
                { "NpcSpawn2", 0x303 },

                { "ActionEffect01", 0x311 },
                { "ActionEffect08", 0x22E },
                { "ActionEffect16", 0x12D },
                { "ActionEffect24", 0x30F },
                { "ActionEffect32", 0x330 },

                { "StatusEffectList", 0x347 },
                { "StatusEffectList3", 0x2C7 },

                { "Examine", 0x87 },
                { "UpdateGearset", 0x9E },
                { "UpdateParty", 0x36F },

                { "UnknownEffect1", 0x146 },
                { "UnknownEffect16", 0x110 },
                { "ActionEffect02", 0x1C5 },
                { "ActionEffect04", 0x23C }
            },
        };
    }
}
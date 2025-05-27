namespace Unscrambler.Constants.Versions;

public partial class GameConstants
{
    public static VersionConstants For72h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.03.27.0000.0000",
            TableOffsets = [0x2169190, 0x217BE70, 0x2188ED0],
            TableSizes = [19256 * 4, 13336 * 4, 25252 * 4],
            TableRadixes = [116, 105, 101],
            TableMax = [166, 127, 250],
            MidTableOffset = 0x2168AE0,
            MidTableSize = 213 * 8,
            DayTableOffset = 0x21A1960,
            DayTableSize = 37 * 4,
            ObfuscationEnabledMode = 224,
            InitZoneOpcode = 0x27C,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x3A4 },
                { "NpcSpawn", 0x35C },
                { "NpcSpawn2", 0x222 },

                { "ActionEffect01", 0x1BF },
                { "ActionEffect08", 0x104 },
                { "ActionEffect16", 0x2B8 },
                { "ActionEffect24", 0x16C },
                { "ActionEffect32", 0x217 },

                { "StatusEffectList", 0x1A8 },
                { "StatusEffectList3", 0x3C2 },

                { "Examine", 0x372 },
                { "UpdateGearset", 0x1D4 },
                { "UpdateParty", 0x38C },
                { "ActorControl", 0x26F },

                { "UnknownEffect01", 0x284 },
                { "UnknownEffect16", 0x144 },
                { "ActionEffect02", 0xDB },
                { "ActionEffect04", 0x2A4 }
            },
        };
    }
}
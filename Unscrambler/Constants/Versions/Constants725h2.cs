namespace Unscrambler.Constants.Versions;

public partial class GameConstants
{
    public static VersionConstants For725h2()
    {
        return new VersionConstants
        {
            GameVersion = "2025.06.19.0000.0000",
            TableOffsets = [0x216C820, 0x217EB00, 0x218E730],
            TableSizes = [18616 * 4, 16140 * 4, 24240 * 4],
            TableRadixes = [82, 99, 101],
            TableMax = [227, 163, 240],
            MidTableOffset = 0x216C050,
            MidTableSize = 249 * 8,
            DayTableOffset = 0x21A61F0,
            DayTableSize = 48 * 4,
            ObfuscationEnabledMode = 41,
            InitZoneOpcode = 0x37E,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x32A },
                { "NpcSpawn", 0x38C },
                { "NpcSpawn2", 0x81 },

                { "ActionEffect01", 0xBC },
                { "ActionEffect08", 0x22C },
                { "ActionEffect16", 0x108 },
                { "ActionEffect24", 0x123 },
                { "ActionEffect32", 0x10E },

                { "StatusEffectList", 0x2EC },
                { "StatusEffectList3", 0x3B6 },

                { "Examine", 0x364 },
                { "UpdateGearset", 0x1C4 },
                { "UpdateParty", 0x241 },
                { "ActorControl", 0x3E7 },

                { "UnknownEffect01", 0x24C },
                { "UnknownEffect16", 0x2A1 },
                { "ActionEffect02", 0x352 },
                { "ActionEffect04", 0x1C0 }
            }
        };
    }
}
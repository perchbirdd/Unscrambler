namespace Unscrambler.Constants.Versions;

public partial class GameConstants
{
    public static VersionConstants For725()
    {
        return new VersionConstants
        {
            GameVersion = "2025.05.17.0000.0000",
            TableOffsets = [0x216BE40, 0x21808E0, 0x218B6D0],
            TableSizes = [21160 * 4, 11132 * 4, 29700 * 4],
            TableRadixes = [92, 121, 135],
            TableMax = [230, 92, 220],
            MidTableOffset = 0x216B660,
            MidTableSize = 252 * 8,
            DayTableOffset = 0x21A86E0,
            DayTableSize = 42 * 4,
            ObfuscationEnabledMode = 113,
            InitZoneOpcode = 0x161,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0xA9 },
                { "NpcSpawn", 0x2E4 },
                { "NpcSpawn2", 0x257 },

                { "ActionEffect01", 0x16C },
                { "ActionEffect08", 0x157 },
                { "ActionEffect16", 0x393 },
                { "ActionEffect24", 0x6A },
                { "ActionEffect32", 0x3B6 },

                { "StatusEffectList", 0x2CC },
                { "StatusEffectList3", 0x212 },

                { "Examine", 0x31E },
                { "UpdateGearset", 0x348 },
                { "UpdateParty", 0x347 },
                { "ActorControl", 0x110 },

                { "UnknownEffect01", 0x142 },
                { "UnknownEffect16", 0x164 },
                { "ActionEffect02", 0x378 },
                { "ActionEffect04", 0xE3 }
            }
        };
    }
}
namespace Unscrambler.Constants.Versions;

public partial class GameConstants
{
    public static VersionConstants For725h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.06.10.0000.0000",
            TableOffsets = [0x216AFB0, 0x2173A20, 0x218C980],
            TableSizes = [8860 * 4, 25560 * 4, 29890 * 4],
            TableRadixes = [86, 120, 123],
            TableMax = [103, 213, 243],
            MidTableOffset = 0x216ACE0,
            MidTableSize = 89 * 8,
            DayTableOffset = 0x21A9C90,
            DayTableSize = 51 * 4,
            ObfuscationEnabledMode = 50,
            InitZoneOpcode = 0xCE,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x266 },
                { "NpcSpawn", 0x3BF },
                { "NpcSpawn2", 0x393 },

                { "ActionEffect01", 0x1E1 },
                { "ActionEffect08", 0x1DB },
                { "ActionEffect16", 0x1E9 },
                { "ActionEffect24", 0x3A8 },
                { "ActionEffect32", 0x264 },

                { "StatusEffectList", 0x86 },
                { "StatusEffectList3", 0x24B },

                { "Examine", 0x1A6 },
                { "UpdateGearset", 0xBE },
                { "UpdateParty", 0x39B },
                { "ActorControl", 0x28C },

                { "UnknownEffect01", 0x36B },
                { "UnknownEffect16", 0x187 },
                { "ActionEffect02", 0x2DC },
                { "ActionEffect04", 0x1BB }
            }
        };
    }
}
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For725h3()
    {
        return new VersionConstants
        {
            GameVersion = "2025.06.28.0000.0000",
            TableOffsets = [0x216C6F0, 0x2180530, 0x21917E0],
            TableSizes = [20368 * 4, 17580 * 4, 12780 * 4],
            TableRadixes = [93, 94, 113],
            TableMax = [219, 187, 113],
            MidTableOffset = 0x216C5D0,
            MidTableSize = 36 * 8,
            DayTableOffset = 0x219DF70,
            DayTableSize = 33 * 4,
            ObfuscationEnabledMode = 118,
            InitZoneOpcode = 0x332,
            UnknownObfuscationInitOpcode = 0x2AA,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x1D5 },
                { "NpcSpawn", 0x99 },
                { "NpcSpawn2", 0xB1 },

                { "ActionEffect01", 0x125 },
                { "ActionEffect08", 0x249 },
                { "ActionEffect16", 0x243 },
                { "ActionEffect24", 0x31F },
                { "ActionEffect32", 0x14E },

                { "StatusEffectList", 0x209 },
                { "StatusEffectList3", 0x37E },

                { "Examine", 0x278 },
                { "UpdateGearset", 0x34B },
                { "UpdateParty", 0x182 },
                { "ActorControl", 0x3E7 },
                { "ActorCast", 0x113 },

                { "UnknownEffect01", 0x2E7 },
                { "UnknownEffect16", 0x22A },
                { "ActionEffect02", 0x1A7 },
                { "ActionEffect04", 0x8B }
            }
        };
    }
}
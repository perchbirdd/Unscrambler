namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For756h1()
    {
        return new VersionConstants
        {
            GameVersion = "2026.09.15.0000.0000",
            TableOffsets = [0x22EB5B0, 0x22F58C0, 0x2304910],
            TableSizes = [10434 * 4, 15379 * 4, 14175 * 4],
            TableRadixes = [94, 91, 105],
            TableMax = [111, 169, 135],
            MidTableOffset = 0x22EAE50,
            MidTableSize = 236 * 8,
            DayTableOffset = 0x2312690,
            DayTableSize = 44 * 4,
            OpcodeKeyTableSize = 165 * 4,
            OpcodeKeyTableOffset = 0x2312740,
            ObfuscationEnabledMode = 176,
            InitZoneOpcode = 0x32B,
            UnknownObfuscationInitOpcode = 0x2A4,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x1C4 },
                { "NpcSpawn", 0x20C },
                { "NpcSpawn2", 0x1BD },

                { "ActionEffect01", 0x313 },
                { "ActionEffect08", 0x21C },
                { "ActionEffect16", 0x8C },
                { "ActionEffect24", 0x30A },
                { "ActionEffect32", 0x3AA },

                { "StatusEffectList", 0x83 },
                { "StatusEffectList3", 0x3DC },

                { "Examine", 0x1F2 },
                { "UpdateGearset", 0x10F },
                { "UpdateParty", 0x191 },
                { "ActorControl", 0x25F },
                { "ActorCast", 0x162 },

                { "UnknownEffect01", 0x3BD },
                { "UnknownEffect16", 0x27D },
                { "ActionEffect02", 0x30B },
                { "ActionEffect04", 0x292 }
            },
        };
    }
}

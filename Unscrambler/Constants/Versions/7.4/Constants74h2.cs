namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For74h2()
    {
        return new VersionConstants
        {
            GameVersion = "2025.12.23.0000.0000",
            TableOffsets = [0x21EF370, 0x21FF410, 0x220AD90],
            TableSizes = [16422 * 4, 11869 * 4, 26040 * 4],
            TableRadixes = [119, 83, 105],
            TableMax = [138, 143, 248],
            MidTableOffset = 0x21EEC70,
            MidTableSize = 224 * 8,
            DayTableOffset = 0x2224470,
            DayTableSize = 28 * 4,
            OpcodeKeyTableSize = 108 * 4,
            OpcodeKeyTableOffset = 0x22244E0,
            ObfuscationEnabledMode = 29,
            InitZoneOpcode = 0x242,
            UnknownObfuscationInitOpcode = 0x318,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0xCA },
                { "NpcSpawn", 0x39B },
                { "NpcSpawn2", 0x1D0 },

                { "ActionEffect01", 0x3D8 },
                { "ActionEffect08", 0x20D },
                { "ActionEffect16", 0x231 },
                { "ActionEffect24", 0x261 },
                { "ActionEffect32", 0x22F },

                { "StatusEffectList", 0x253 },
                { "StatusEffectList3", 0x29C },

                { "Examine", 0x70 },
                { "UpdateGearset", 0x185 },
                { "UpdateParty", 0x37E },
                { "ActorControl", 0x19B },
                { "ActorCast", 0x2F2 },

                { "UnknownEffect01", 0xC6 },
                { "UnknownEffect16", 0x241 },
                { "ActionEffect02", 0x392 },
                { "ActionEffect04", 0x3E5 }
            },
        };
    }
}

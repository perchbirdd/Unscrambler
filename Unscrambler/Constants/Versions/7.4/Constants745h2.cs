namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For745h2()
    {
        return new VersionConstants
        {
            GameVersion = "2026.03.17.0000.0000",
            TableOffsets = [0x21F46A0, 0x22023E0, 0x2208C80],
            TableSizes = [14157 * 4, 6696 * 4, 19920 * 4],
            TableRadixes = [121, 108, 120],
            TableMax = [117, 62, 166],
            MidTableOffset = 0x21F4290,
            MidTableSize = 129 * 8,
            DayTableOffset = 0x221C3C0,
            DayTableSize = 59 * 4,
            OpcodeKeyTableSize = 176 * 4,
            OpcodeKeyTableOffset = 0x221C4B0,
            ObfuscationEnabledMode = 96,
            InitZoneOpcode = 0x2F2,
            UnknownObfuscationInitOpcode = 0x107,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x109 },
                { "NpcSpawn", 0x328 },
                { "NpcSpawn2", 0xAE },

                { "ActionEffect01", 0x2A4 },
                { "ActionEffect08", 0x283 },
                { "ActionEffect16", 0x190 },
                { "ActionEffect24", 0x200 },
                { "ActionEffect32", 0x164 },

                { "StatusEffectList", 0x234 },
                { "StatusEffectList3", 0x388 },

                { "Examine", 0x14D },
                { "UpdateGearset", 0x212 },
                { "UpdateParty", 0x145 },
                { "ActorControl", 0x311 },
                { "ActorCast", 0x3B9 },

                { "UnknownEffect01", 0x321 },
                { "UnknownEffect16", 0x278 },
                { "ActionEffect02", 0xB6 },
                { "ActionEffect04", 0x35B }
            },
        };
    }
}
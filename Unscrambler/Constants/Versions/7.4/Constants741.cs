namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For741()
    {
        return new VersionConstants
        {
            GameVersion = "2026.01.21.0000.0000",
            TableOffsets = [0x21F0DB0, 0x21F76E0, 0x2204CD0],
            TableSizes = [6732 * 4, 13689 * 4, 29700 * 4],
            TableRadixes = [102, 81, 135],
            TableMax = [66, 169, 220],
            MidTableOffset = 0x21F0810,
            MidTableSize = 179 * 8,
            DayTableOffset = 0x2221CE0,
            DayTableSize = 32 * 4,
            OpcodeKeyTableSize = 51 * 4,
            OpcodeKeyTableOffset = 0x2221D60,
            ObfuscationEnabledMode = 249,
            InitZoneOpcode = 0x1A4,
            UnknownObfuscationInitOpcode = 0x88,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x13E },
                { "NpcSpawn", 0x2E8 },
                { "NpcSpawn2", 0x212 },

                { "ActionEffect01", 0x1E7 },
                { "ActionEffect08", 0x77 },
                { "ActionEffect16", 0x38F },
                { "ActionEffect24", 0xB2 },
                { "ActionEffect32", 0x7C },

                { "StatusEffectList", 0x31E },
                { "StatusEffectList3", 0xEB },

                { "Examine", 0x235 },
                { "UpdateGearset", 0x91 },
                { "UpdateParty", 0x1BA },
                { "ActorControl", 0x2C8 },
                { "ActorCast", 0x1A1 },

                { "UnknownEffect01", 0xFC },
                { "UnknownEffect16", 0x1C6 },
                { "ActionEffect02", 0xE4 },
                { "ActionEffect04", 0x10C }
            },
        };
    }
}

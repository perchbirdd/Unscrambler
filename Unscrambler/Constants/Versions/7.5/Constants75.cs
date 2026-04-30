namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For75()
    {
        return new VersionConstants
        {
            GameVersion = "2026.04.21.0000.0000",
            TableOffsets = [0x22E1870, 0x22F3C60, 0x22FEDA0],
            TableSizes = [18683 * 4, 11342 * 4, 24500 * 4],
            TableRadixes = [119, 107, 100],
            TableMax = [157, 106, 245],
            MidTableOffset = 0x22E11F0,
            MidTableSize = 207 * 8,
            DayTableOffset = 0x2316C70,
            DayTableSize = 22 * 4,
            OpcodeKeyTableSize = 182 * 4,
            OpcodeKeyTableOffset = 0x2316CD0,
            ObfuscationEnabledMode = 31,
            InitZoneOpcode = 0x98,
            UnknownObfuscationInitOpcode = 0x108,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x343 },
                { "NpcSpawn", 0x2AE },
                { "NpcSpawn2", 0x249 },

                { "ActionEffect01", 0x39A },
                { "ActionEffect08", 0x1F2 },
                { "ActionEffect16", 0x240 },
                { "ActionEffect24", 0x1CA },
                { "ActionEffect32", 0x3C8 },

                { "StatusEffectList", 0x117 },
                { "StatusEffectList3", 0x336 },

                { "Examine", 0x100 },
                { "UpdateGearset", 0x9E },
                { "UpdateParty", 0x239 },
                { "ActorControl", 0x328 },
                { "ActorCast", 0x345 },

                { "UnknownEffect01", 0x174 },
                { "UnknownEffect16", 0x163 },
                { "ActionEffect02", 0x395 },
                { "ActionEffect04", 0x3AE }
            },
        };
    }
}
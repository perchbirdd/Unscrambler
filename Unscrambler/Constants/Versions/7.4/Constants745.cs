namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For745()
    {
        return new VersionConstants
        {
            GameVersion = "2026.02.20.0000.0000",
            TableOffsets = [0x21F31E0, 0x2203590, 0x220CA60],
            TableSizes = [16617 * 4, 9523 * 4, 17484 * 4],
            TableRadixes = [87, 107, 124],
            TableMax = [191, 89, 141],
            MidTableOffset = 0x21F2EA0,
            MidTableSize = 103 * 8,
            DayTableOffset = 0x221DB90,
            DayTableSize = 33 * 4,
            OpcodeKeyTableSize = 35 * 4,
            OpcodeKeyTableOffset = 0x221DC20,
            ObfuscationEnabledMode = 108,
            InitZoneOpcode = 0x9D,
            UnknownObfuscationInitOpcode = 0x37F,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x1A4 },
                { "NpcSpawn", 0x304 },
                { "NpcSpawn2", 0x379 },

                { "ActionEffect01", 0x2FA },
                { "ActionEffect08", 0x3BE },
                { "ActionEffect16", 0x228 },
                { "ActionEffect24", 0x26F },
                { "ActionEffect32", 0x210 },

                { "StatusEffectList", 0x6F },
                { "StatusEffectList3", 0x330 },

                { "Examine", 0x8B },
                { "UpdateGearset", 0x239 },
                { "UpdateParty", 0x2A1 },
                { "ActorControl", 0x114 },
                { "ActorCast", 0x3E0 },

                { "UnknownEffect01", 0x3D6 },
                { "UnknownEffect16", 0x2CB },
                { "ActionEffect02", 0xD0 },
                { "ActionEffect04", 0x13A }
            },
        };
    }
}
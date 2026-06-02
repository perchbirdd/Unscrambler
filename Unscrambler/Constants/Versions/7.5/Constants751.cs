namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For751()
    {
        return new VersionConstants
        {
            GameVersion = "2026.05.25.0000.0000",
            TableOffsets = [0x22E1490, 0x22ECC30, 0x22FDDA0],
            TableSizes = [11752 * 4, 17500 * 4, 14496 * 4],
            TableRadixes = [113, 125, 96],
            TableMax = [104, 140, 151],    
            MidTableOffset = 0x22E1020,
            MidTableSize = 142 * 8,
            DayTableOffset = 0x230C020,
            DayTableSize = 51 * 4,
            OpcodeKeyTableSize = 143 * 4,
            OpcodeKeyTableOffset = 0x230C0F0,
            ObfuscationEnabledMode = 159,
            InitZoneOpcode = 0x96,
            UnknownObfuscationInitOpcode = 0xA3,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x255 },
                { "NpcSpawn", 0x26B },
                { "NpcSpawn2", 0x242 },

                { "ActionEffect01", 0x393 },
                { "ActionEffect08", 0xD3 },
                { "ActionEffect16", 0xA7 },
                { "ActionEffect24", 0x147 },
                { "ActionEffect32", 0x3D1 },

                { "StatusEffectList", 0x295 },
                { "StatusEffectList3", 0x138 },

                { "Examine", 0x3A2 },
                { "UpdateGearset", 0x127 },
                { "UpdateParty", 0xEB },
                { "ActorControl", 0x1E8 },
                { "ActorCast", 0x2F6 },

                { "UnknownEffect01", 0x39B },
                { "UnknownEffect16", 0x269 },
                { "ActionEffect02", 0x157 },
                { "ActionEffect04", 0xEF }
            },
        };
    }
}
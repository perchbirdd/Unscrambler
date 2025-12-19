namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For74h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.12.18.0000.0000",
            TableOffsets = [0x21EEE80, 0x2200CE0, 0x2218A10],
            TableSizes = [18328 * 4, 24395 * 4, 10856 * 4],
            TableRadixes = [116, 119, 118],
            TableMax = [158, 205, 92],
            MidTableOffset = 0x21EE870,
            MidTableSize = 193 * 8,
            DayTableOffset = 0x22233B0,
            DayTableSize = 23 * 4,
            OpcodeKeyTableSize = 98 * 4,
            OpcodeKeyTableOffset = 0x2223410,
            ObfuscationEnabledMode = 26,
            InitZoneOpcode = 0x12E,
            UnknownObfuscationInitOpcode = 0x354,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x18B },
                { "NpcSpawn", 0x11E },
                { "NpcSpawn2", 0x2BD },

                { "ActionEffect01", 0xC1 },
                { "ActionEffect08", 0x3D6 },
                { "ActionEffect16", 0x324 },
                { "ActionEffect24", 0x243 },
                { "ActionEffect32", 0x120 },

                { "StatusEffectList", 0x3D7 },
                { "StatusEffectList3", 0x101 },

                { "Examine", 0xCE },
                { "UpdateGearset", 0x2BB },
                { "UpdateParty", 0x83 },
                { "ActorControl", 0xD8 },
                { "ActorCast", 0x385 },

                { "UnknownEffect01", 0x2D8 },
                { "UnknownEffect16", 0x1FD },
                { "ActionEffect02", 0x16F },
                { "ActionEffect04", 0x186 }
            },
        };
    }
}
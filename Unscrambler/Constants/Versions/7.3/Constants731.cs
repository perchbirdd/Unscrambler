namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For731()
    {
        return new VersionConstants
        {
            GameVersion = "2025.08.22.0000.0000",
            TableOffsets = [0x2188BB0, 0x219F590, 0x21ABF60],
            TableSizes = [23160 * 4, 12916 * 4, 25432 * 4],
            TableRadixes = [120, 123, 136],
            TableMax = [193, 105, 187],
            MidTableOffset = 0x2188480,
            MidTableSize = 230 * 8,
            DayTableOffset = 0x21C4CC0,
            DayTableSize = 44 * 4,
            OpcodeKeyTableOffset = 0x21C4D70,
            OpcodeKeyTableSize = 55 * 4,
            ObfuscationEnabledMode = 203,
            InitZoneOpcode = 0x2D0,
            UnknownObfuscationInitOpcode = 0x28D,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x1B3 },
                { "NpcSpawn", 0x18D },
                { "NpcSpawn2", 0x2CD },

                { "ActionEffect01", 0x219 },
                { "ActionEffect08", 0x273 },
                { "ActionEffect16", 0x1D0 },
                { "ActionEffect24", 0x11A },
                { "ActionEffect32", 0x140 },

                { "StatusEffectList", 0xBA },
                { "StatusEffectList3", 0x3D2 },

                { "Examine", 0x2D5 },
                { "UpdateGearset", 0x36C },
                { "UpdateParty", 0x3DC },
                { "ActorControl", 0x202 },
                { "ActorCast", 0x38A },

                { "UnknownEffect01", 0x7A },
                { "UnknownEffect16", 0x8F },
                { "ActionEffect02", 0x25A },
                { "ActionEffect04", 0x3D4 }
            }
        };
    }
}
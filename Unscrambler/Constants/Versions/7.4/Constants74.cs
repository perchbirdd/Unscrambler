
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For74()
    {
        return new VersionConstants
        {
            GameVersion = "2025.12.09.0000.0000",
            TableOffsets = [0x21F04E0, 0x21F9EA0, 0x2204F00],
            TableSizes = [9840 * 4, 11286 * 4, 17934 * 4],
            TableRadixes = [82, 99, 122],
            TableMax = [120, 114, 147],
            MidTableOffset = 0x21F0360,
            MidTableSize = 48 * 8,
            DayTableOffset = 0x2216740,
            DayTableSize = 39 * 4,
            OpcodeKeyTableOffset = 0x22167E0,
            OpcodeKeyTableSize = 217 * 4,
            ObfuscationEnabledMode = 182,
            InitZoneOpcode = 0x0,
            UnknownObfuscationInitOpcode = 0x259,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x107 },
                { "NpcSpawn", 0x2FC },
                { "NpcSpawn2", 0x28C },
                
                { "ActionEffect01", 0x1C3 },
                { "ActionEffect08", 0x85 },
                { "ActionEffect16", 0x27F },
                { "ActionEffect24", 0x20F },
                { "ActionEffect32", 0x2F3 },
                
                { "StatusEffectList", 0x2C4 },
                { "StatusEffectList3", 0xD8 },
                
                { "Examine", 0x1FF },
                { "UpdateGearset", 0x2B4 },
                { "UpdateParty", 0x240 },
                { "ActorControl", 0x13C },
                { "ActorCast", 0x1A9 },
                
                { "UnknownEffect01", 0x3CE },
                { "UnknownEffect16", 0x3CD },
                { "ActionEffect02", 0x28D },
                { "ActionEffect04", 0x187 }
            }
        };
    }
}
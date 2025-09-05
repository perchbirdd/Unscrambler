
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For731h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.09.04.0000.0000",
            TableOffsets = [0x2187210, 0x219F690, 0x21A4600],
            TableSizes = [24864 * 4, 5084 * 4, 28000 * 4],
            TableRadixes = [112, 82, 112],
            TableMax = [222, 62, 250],
            MidTableOffset = 0x2186E10,
            MidTableSize = 128 * 8,
            DayTableOffset = 0x21BFB80,
            DayTableSize = 59 * 4,
            OpcodeKeyTableOffset = 0x21BFC70,
            OpcodeKeyTableSize = 144 * 4,
            ObfuscationEnabledMode = 46,
            InitZoneOpcode = 0x309,
            UnknownObfuscationInitOpcode = 0x234,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x21E },
                { "NpcSpawn", 0xE0 },
                { "NpcSpawn2", 0x1B5 },
                
                { "ActionEffect01", 0x338 },
                { "ActionEffect08", 0x1E8 },
                { "ActionEffect16", 0xD6 },
                { "ActionEffect24", 0x240 },
                { "ActionEffect32", 0x12A },
                
                { "StatusEffectList", 0x12C },
                { "StatusEffectList3", 0x307 },
                
                { "Examine", 0x1AF },
                { "UpdateGearset", 0x273 },
                { "UpdateParty", 0x6B },
                { "ActorControl", 0x3D1 },
                { "ActorCast", 0xCC },
                
                { "UnknownEffect01", 0x26A },
                { "UnknownEffect16", 0x270 },
                { "ActionEffect02", 0x272 },
                { "ActionEffect04", 0x1E3 }
            }
        };
    }
}
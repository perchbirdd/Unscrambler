
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For738()
    {
        return new VersionConstants
        {
            GameVersion = "2025.10.30.0000.0000",
            TableOffsets = [0x2186D40, 0x219C750, 0x21A1BD0],
            TableSizes = [22145 * 4, 5406 * 4, 24797 * 4],
            TableRadixes = [103, 102, 137],
            TableMax = [215, 53, 181],
            MidTableOffset = 0x21868C0,
            MidTableSize = 143 * 8,
            DayTableOffset = 0x21B9F50,
            DayTableSize = 57 * 4,
            OpcodeKeyTableOffset = 0x21BA040,
            OpcodeKeyTableSize = 77 * 4,
            ObfuscationEnabledMode = 49,
            InitZoneOpcode = 0x2B1,
            UnknownObfuscationInitOpcode = 0x1BC,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x270 },
                { "NpcSpawn", 0x2C9 },
                { "NpcSpawn2", 0xC4 },
                
                { "ActionEffect01", 0x2AB },
                { "ActionEffect08", 0x242 },
                { "ActionEffect16", 0xED },
                { "ActionEffect24", 0x9C },
                { "ActionEffect32", 0x225 },
                
                { "StatusEffectList", 0xB0 },
                { "StatusEffectList3", 0x10A },
                
                { "Examine", 0x13A },
                { "UpdateGearset", 0x363 },
                { "UpdateParty", 0x369 },
                { "ActorControl", 0x113 },
                { "ActorCast", 0x32D },
                
                { "UnknownEffect01", 0x206 },
                { "UnknownEffect16", 0x3D3 },
                { "ActionEffect02", 0xBE },
                { "ActionEffect04", 0x66 }
            }
        };
    }
}
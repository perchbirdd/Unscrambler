namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For73h1()
    {
        return new VersionConstants
        {
            GameVersion = "2025.08.07.0000.0000",
            TableOffsets = [0x2188D90, 0x2191670, 0x219A080],
            TableSizes = [8760 * 4, 8836 * 4, 21616 * 4],
            TableRadixes = [120, 93, 112],
            TableMax = [73, 95, 193],
            MidTableOffset = 0x2188C20,
            MidTableSize = 46 * 8,
            DayTableOffset = 0x21AF240,
            DayTableSize = 30 * 4,
            OpcodeKeyTableOffset = 0x21AF2C0,
            OpcodeKeyTableSize = 51 * 4,
            ObfuscationEnabledMode = 21,
            InitZoneOpcode = 0x36C,
            UnknownObfuscationInitOpcode = 0x2FB,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x37A },
                { "NpcSpawn", 0xC4 },
                { "NpcSpawn2", 0x1E0 },
            
                { "ActionEffect01", 0x247 },
                { "ActionEffect08", 0x372 },
                { "ActionEffect16", 0x2F3 },
                { "ActionEffect24", 0x31A },
                { "ActionEffect32", 0x35D },
            
                { "StatusEffectList", 0x2C9 },
                { "StatusEffectList3", 0x100 },
            
                { "Examine", 0x13F },
                { "UpdateGearset", 0x15C },
                { "UpdateParty", 0x3DF },
                { "ActorControl", 0x2E0 },
                { "ActorCast", 0x26F },
            
                { "UnknownEffect01", 0xE7 },
                { "UnknownEffect16", 0x1C6 },
                { "ActionEffect02", 0xD7 },
                { "ActionEffect04", 0x259 }
            }
        };
    }
}
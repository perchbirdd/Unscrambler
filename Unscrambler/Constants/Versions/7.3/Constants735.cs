
namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For735()
    {
        return new VersionConstants
        {

            GameVersion = "2025.09.30.0000.0000",
            TableOffsets = [0x2188190, 0x218DA90, 0x219FB40],
            TableSizes = [5696 * 4, 18478 * 4, 9008 * 4],
            TableRadixes = [89, 91, 100],
            TableMax = [64, 203, 90],
            MidTableOffset = 0x2187E60,
            MidTableSize = 102 * 8,
            DayTableOffset = 0x21A87E0,
            DayTableSize = 50 * 4,
            OpcodeKeyTableOffset = 0x21A88B0,
            OpcodeKeyTableSize = 225 * 4,
            ObfuscationEnabledMode = 52,
            InitZoneOpcode = 0x173,
            UnknownObfuscationInitOpcode = 0x312,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0xFA },
                { "NpcSpawn", 0x23F },
                { "NpcSpawn2", 0x383 },
                { "ActionEffect01", 0x1DA },
                { "ActionEffect08", 0x335 },
                { "ActionEffect16", 0x33E },
                { "ActionEffect24", 0x12A },
                { "ActionEffect32", 0x304 },
                { "StatusEffectList", 0x68 },
                { "StatusEffectList3", 0x2F8 },
                { "Examine", 0x208 },
                { "UpdateGearset", 0x38F },
                { "UpdateParty", 0x14A },
                { "ActorControl", 0x31B },
                { "ActorCast", 0x20F },
                { "UnknownEffect01", 0x310 },
                { "UnknownEffect16", 0x1FD },
                { "ActionEffect02", 0x1F5 },
                { "ActionEffect04", 0x2AA }
            }
        };
    }
}
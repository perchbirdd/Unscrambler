namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    public static VersionConstants For73()
    {
        return new VersionConstants
        {
            GameVersion = "2025.07.30.0000.0000",
            TableOffsets = [0x2188B40, 0x218EA50, 0x21A8970],
            TableSizes = [6128 * 4, 26596 * 4, 13964 * 4],
            TableRadixes = [79, 108, 129],
            TableMax = [77, 246, 108],
            MidTableOffset = 0x2188660,
            MidTableSize = 155 * 8,
            DayTableOffset = 0x21B6320,
            DayTableSize = 55 * 4,
            OpcodeKeyTableOffset = 0x21B6400,
            OpcodeKeyTableSize = 85 * 4,
            ObfuscationEnabledMode = 83,
            InitZoneOpcode = 0x16C,
            UnknownObfuscationInitOpcode = 0x176,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x1D0 },
                { "NpcSpawn", 0x22D },
                { "NpcSpawn2", 0x344 },

                { "ActionEffect01", 0x301 },
                { "ActionEffect08", 0x1B0 },
                { "ActionEffect16", 0x1E0 },
                { "ActionEffect24", 0x221 },
                { "ActionEffect32", 0x2B6 },

                { "StatusEffectList", 0x397 },
                { "StatusEffectList3", 0x292 },

                { "Examine", 0x1CA },
                { "UpdateGearset", 0x2AC },
                { "UpdateParty", 0x350 },
                { "ActorControl", 0xE4 },
                { "ActorCast", 0x1BD },

                { "UnknownEffect01", 0x1A9 },
                { "UnknownEffect16", 0x213 },
                { "ActionEffect02", 0x1C8 },
                { "ActionEffect04", 0x7B }
            }
        };
    }
}
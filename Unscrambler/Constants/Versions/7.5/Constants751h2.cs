namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For751h2()
    {
        return new VersionConstants
        {
            GameVersion = "2026.06.18.0000.0000",
            TableOffsets = [0x22DF9E0, 0x22E99D0, 0x22F1FB0],
            TableSizes = [10234 * 4, 8568 * 4, 15808 * 4],
            TableRadixes = [119, 119, 104],
            TableMax = [86, 72, 152],
            MidTableOffset = 0x22DF6A0,
            MidTableSize = 104 * 8,
            DayTableOffset = 0x23016B0,
            DayTableSize = 49 * 4,
            OpcodeKeyTableSize = 105 * 4,
            OpcodeKeyTableOffset = 0x2301780,
            ObfuscationEnabledMode = 72,
            InitZoneOpcode = 0x337,
            UnknownObfuscationInitOpcode = 0xD7,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x2E0 },
                { "NpcSpawn", 0x378 },
                { "NpcSpawn2", 0x188 },

                { "ActionEffect01", 0x37D },
                { "ActionEffect08", 0x350 },
                { "ActionEffect16", 0x27E },
                { "ActionEffect24", 0x1A4 },
                { "ActionEffect32", 0x2A2 },

                { "StatusEffectList", 0x132 },
                { "StatusEffectList3", 0x28B },

                { "Examine", 0x32C },
                { "UpdateGearset", 0x333 },
                { "UpdateParty", 0x3DC },
                { "ActorControl", 0x19F },
                { "ActorCast", 0x1C9 },

                { "UnknownEffect01", 0x144 },
                { "UnknownEffect16", 0x346 },
                { "ActionEffect02", 0x1DA },
                { "ActionEffect04", 0x2B8 }
            },
        };
    }
}
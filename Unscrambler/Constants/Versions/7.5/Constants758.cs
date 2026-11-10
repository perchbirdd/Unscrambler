namespace Unscrambler.Constants.Versions;

public static partial class GameConstants
{
    [VersionConstant]
    public static VersionConstants For758()
    {
        return new VersionConstants
        {
            GameVersion = "2026.11.03.0000.0000",
            TableOffsets = [0x22EFA20, 0x22F9F70, 0x2306970],
            TableSizes = [10578 * 4, 12927 * 4, 28251 * 4],
            TableRadixes = [86, 93, 129],
            TableMax = [123, 139, 219],
            MidTableOffset = 0x22EF720,
            MidTableSize = 96 * 8,
            DayTableOffset = 0x23222E0,
            DayTableSize = 25 * 4,
            OpcodeKeyTableSize = 34 * 4,
            OpcodeKeyTableOffset = 0x2322350,
            ObfuscationEnabledMode = 157,
            InitZoneOpcode = 0xAB,
            UnknownObfuscationInitOpcode = 0x2F5,
            ObfuscatedOpcodes = new Dictionary<string, int>
            {
                { "PlayerSpawn", 0x2FC },
                { "NpcSpawn", 0x176 },
                { "NpcSpawn2", 0x2FD },

                { "ActionEffect01", 0x190 },
                { "ActionEffect08", 0x233 },
                { "ActionEffect16", 0x119 },
                { "ActionEffect24", 0x39C },
                { "ActionEffect32", 0xDD },

                { "StatusEffectList", 0x15D },
                { "StatusEffectList3", 0x35D },

                { "Examine", 0x289 },
                { "UpdateGearset", 0x172 },
                { "UpdateParty", 0x3E3 },
                { "ActorControl", 0x2DF },
                { "ActorCast", 0x160 },

                { "UnknownEffect01", 0x351 },
                { "UnknownEffect16", 0x385 },
                { "ActionEffect02", 0x326 },
                { "ActionEffect04", 0x222 }
            },
        };
    }
}

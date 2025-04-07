using Unscrambler.Constants.Versions;

namespace Unscrambler.Constants;

public class VersionConstants
{
    public string GameVersion { get; init; }

    public byte ObfuscationEnabledMode { get; init; }

    public long[] TableOffsets { get; init; } = [];
    public int[] TableSizes { get; init; } = [];
    public int[] TableRadixes { get; init; } = [];
    public int[] TableMax { get; init; } = [];

    public long MidTableOffset { get; init; }
    public int MidTableSize { get; init; }

    public long DayTableOffset { get; init; }
    public int DayTableSize { get; init; }

    public int InitZoneOpcode { get; init; }

    public Dictionary<string, int> ObfuscatedOpcodes { get; init; } = [];
    public static Dictionary<string, VersionConstants> Constants { get; } = [];

    static VersionConstants()
    {
        var _72x1 = GameConstants.For72h1();
        Constants.Add(_72x1.GameVersion, _72x1);
    }

    public static VersionConstants ForGameVersion(string gameVersion)
    {
        return Constants[gameVersion];
    }
}
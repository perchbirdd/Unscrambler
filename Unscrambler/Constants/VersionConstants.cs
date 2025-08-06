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
    
    public long OpcodeKeyTableOffset { get; init; }
    public int OpcodeKeyTableSize { get; init; }

    public int InitZoneOpcode { get; init; }
    public int UnknownObfuscationInitOpcode { get; init; }

    public Dictionary<string, int> ObfuscatedOpcodes { get; init; } = [];
    public static Dictionary<string, VersionConstants> Constants { get; } = [];

    static VersionConstants()
    {
        var _72 = GameConstants.For72();
        Constants.Add(_72.GameVersion, _72);
        var _72x1 = GameConstants.For72h1();
        Constants.Add(_72x1.GameVersion, _72x1);
        var _721 = GameConstants.For721();
        Constants.Add(_721.GameVersion, _721);
        var _725 = GameConstants.For725();
        Constants.Add(_725.GameVersion, _725);
        var _725h1 = GameConstants.For725h1();
        Constants.Add(_725h1.GameVersion, _725h1);
        var _725h2 = GameConstants.For725h2();
        Constants.Add(_725h2.GameVersion, _725h2);
        var _725h3 = GameConstants.For725h3();
        Constants.Add(_725h3.GameVersion, _725h3);
        var _73 = GameConstants.For73();
        Constants.Add(_73.GameVersion, _73);
    }

    public static VersionConstants ForGameVersion(string gameVersion)
    {
        return Constants[gameVersion];
    }
}
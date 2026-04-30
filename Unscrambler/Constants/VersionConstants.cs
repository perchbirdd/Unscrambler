using System.Reflection;

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
        var assembly = Assembly.GetAssembly(typeof(VersionConstants));
        if (assembly == null) return;
        
        foreach (var type in assembly.GetTypes())
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                if (method.GetCustomAttribute<VersionConstantAttribute>() == null) continue;
                var constants = (VersionConstants) method.Invoke(null, null)!;
                Constants.Add(constants.GameVersion, constants);
            }
        }
    }

    public static VersionConstants ForGameVersion(string gameVersion)
    {
        return Constants[gameVersion];
    }
}
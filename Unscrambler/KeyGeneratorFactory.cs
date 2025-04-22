using Unscrambler.Constants;
using Unscrambler.Derivation;
using Unscrambler.Derivation.Versions;

namespace Unscrambler;

public abstract class KeyGeneratorFactory
{
    public static IKeyGenerator ForGameVersion(string gameVersion)
    {
        if (VersionConstants.Constants.TryGetValue(gameVersion, out var constants))
            return Create(constants);
        throw new ArgumentException($"Unsupported game version: {gameVersion}");
    }

    public static IKeyGenerator WithConstants(VersionConstants constants, string tableBinaryBasePath)
    {
        return Create(constants, tableBinaryBasePath);
    }
    
    private static IKeyGenerator Create(VersionConstants constants, string? tableBinaryBasePath = null)
    {
        IKeyGenerator keyGenerator = constants.GameVersion switch
        {
            "2025.03.18.0000.0000" => new KeyGenerator72(),
            "2025.03.27.0000.0000" => new KeyGenerator72(),
            "2025.04.16.0000.0000" => new KeyGenerator72(),
            _ => throw new ArgumentException($"Unsupported game version: {constants.GameVersion}")
        };
        
        keyGenerator.Initialize(constants, tableBinaryBasePath);
        return keyGenerator;
    }
}
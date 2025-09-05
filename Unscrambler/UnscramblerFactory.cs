using Unscrambler.Constants;
using Unscrambler.Unscramble;
using Unscrambler.Unscramble.Versions;

namespace Unscrambler;

public abstract class UnscramblerFactory
{
    public static IUnscrambler ForGameVersion(string gameVersion)
    {
        if (VersionConstants.Constants.TryGetValue(gameVersion, out var constants))
            return Create(constants);
        throw new ArgumentException($"Unsupported game version: {gameVersion}");
    }
    
    private static IUnscrambler Create(VersionConstants constants)
    {
        IUnscrambler unscrambler = constants.GameVersion switch
        {
            "2025.03.18.0000.0000" => new Unscrambler72(),
            "2025.03.27.0000.0000" => new Unscrambler72(),
            "2025.04.16.0000.0000" => new Unscrambler72(),
            "2025.05.17.0000.0000" => new Unscrambler72(),
            "2025.06.10.0000.0000" => new Unscrambler72(),
            "2025.06.19.0000.0000" => new Unscrambler72(),
            "2025.06.28.0000.0000" => new Unscrambler72(),
            "2025.07.30.0000.0000" => new Unscrambler73(),
            "2025.08.07.0000.0000" => new Unscrambler73(),
            "2025.08.22.0000.0000" => new Unscrambler73(),
            "2025.09.04.0000.0000" => new Unscrambler73(),
            _ => throw new ArgumentException($"Unsupported game version: {constants.GameVersion}")
        };
        
        unscrambler.Initialize(constants);
        return unscrambler;
    }
}
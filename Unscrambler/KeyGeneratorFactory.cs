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
    
    public static IKeyGenerator WithConstants(VersionConstants constants, byte[] table0, byte[] table1, byte[] table2, byte[] midTable, byte[] dayTable, byte[]? opcodeKeyTable = null)
    {
        return Create(constants, table0, table1, table2, midTable, dayTable, opcodeKeyTable);
    }

    private static IKeyGenerator GetKeyGenerator(VersionConstants constants) =>
        constants.GameVersion switch
        {
            "2025.03.18.0000.0000" => new KeyGenerator72(),
            "2025.03.27.0000.0000" => new KeyGenerator72(),
            "2025.04.16.0000.0000" => new KeyGenerator72(),
            "2025.05.17.0000.0000" => new KeyGenerator72(),
            "2025.06.10.0000.0000" => new KeyGenerator72(),
            "2025.06.19.0000.0000" => new KeyGenerator72(),
            "2025.06.28.0000.0000" => new KeyGenerator72(),
            "2025.07.30.0000.0000" => new KeyGenerator73(),
            "2025.08.07.0000.0000" => new KeyGenerator73(),
            "2025.08.22.0000.0000" => new KeyGenerator73(),
            "2025.09.04.0000.0000" => new KeyGenerator73(),
            "2025.09.30.0000.0000" => new KeyGenerator73(),
            "2025.10.13.0000.0000" => new KeyGenerator73(),
            "2025.10.30.0000.0000" => new KeyGenerator73(),
            _ => throw new ArgumentException($"Unsupported game version: {constants.GameVersion}")
        };

    private static IKeyGenerator Create(VersionConstants constants, string? tableBinaryBasePath = null)
    {
        IKeyGenerator keyGenerator = GetKeyGenerator(constants);
        keyGenerator.Initialize(constants, tableBinaryBasePath);
        return keyGenerator;
    }
    
    private static IKeyGenerator Create(VersionConstants constants, byte[] table0, byte[] table1, byte[] table2, byte[] midTable, byte[] dayTable, byte[]? opcodeKeyTable = null)
    {
        IKeyGenerator keyGenerator = GetKeyGenerator(constants);
        keyGenerator.Initialize(constants, table0, table1, table2, midTable, dayTable, opcodeKeyTable);
        return keyGenerator;
    }
}

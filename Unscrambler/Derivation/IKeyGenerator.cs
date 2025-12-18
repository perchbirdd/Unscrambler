using Unscrambler.Constants;

namespace Unscrambler.Derivation;

public interface IKeyGenerator
{
    /// <summary>
    /// Initialize this IKeyGenerator.
    /// </summary>
    /// <param name="constants">The constants to use to initialize this IKeyGenerator.</param>
    /// <param name="tableBinaryBasePath">The optional base path to custom table binaries.</param>
    void Initialize(VersionConstants constants, string? tableBinaryBasePath = null);
    
    /// <summary>
    /// Initialize this IKeyGenerator directly from in-memory table binaries.
    /// </summary>
    /// <param name="constants">The constants to use to initialize this IKeyGenerator.</param>
    /// <param name="table0">The Table0 byte array.</param>
    /// <param name="table1">The Table1 byte array.</param>
    /// <param name="table2">The Table2 byte array.</param>
    /// <param name="midTable">The MidTable byte array.</param>
    /// <param name="dayTable">The DayTable byte array.</param>
    /// <param name="opcodeKeyTable">The optional OpcodeKeyTable byte array, required for FFXIV versions 7.3 and later.</param>
    void Initialize(VersionConstants constants, byte[] table0, byte[] table1, byte[] table2, byte[] midTable, byte[] dayTable, byte[]? opcodeKeyTable = null);
    
    /// <summary>
    /// Generate packet obfuscation keys from an InitZone packet. 
    /// </summary>
    /// <param name="initZonePacket">The initzone packet, starting from after the packet header, sometimes called
    /// the segment header.</param>
    /// <exception cref="NotImplementedException">If this IKeyGenerator was provided for
    /// version 7.4 / 2025.12.09.0000.0000 or later.</exception>
    void GenerateFromInitZone(Span<byte> initZonePacket);
    
    /// <summary>
    /// Generate packet obfuscation keys from the unknown packet that can initialize obfuscation. 
    /// </summary>
    /// <param name="unknownPacket">The unknown packet, starting from after the packet header, sometimes called
    /// the segment header.</param>
    void GenerateFromUnknownInitializer(Span<byte> unknownPacket);

    /// <summary>
    /// Generate the opcode-based key for a provided opcode using the keys in this IKeyGenerator.
    /// </summary>
    /// <param name="opcode">The opcode to generate the opcode-based key for.</param>
    /// <returns>The opcode-based key for the provided opcode on the version this IKeyGenerator was requested for.</returns>
    /// <exception cref="NotImplementedException">If this IKeyGenerator was requested for version
    /// 7.25 hotfix 3 / 2025.06.28.0000.0000 or earlier.</exception>
    int GetOpcodeBasedKey(int opcode);
    
    /// <summary>
    /// The current obfuscation enabled setting, provided by the server.
    /// </summary>
    bool ObfuscationEnabled { get; }
    
    /// <summary>
    /// The current generated keys, seeded by the server and derived by this IKeyGenerator.
    /// </summary>
    byte[] Keys { get; }
}

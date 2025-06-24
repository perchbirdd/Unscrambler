using Unscrambler.Constants;

namespace Unscrambler.Derivation;

public interface IKeyGenerator
{
    void Initialize(VersionConstants constants, string? tableBinaryBasePath = null);

    [Obsolete("Please use GenerateFromInitZone")]
    void Generate(Span<byte> initZonePacket);
    void GenerateFromInitZone(Span<byte> initZonePacket);
    void GenerateFromUnknownInitializer(Span<byte> unknownPacket);
    
    bool ObfuscationEnabled { get; }
    byte[] Keys { get; }
}
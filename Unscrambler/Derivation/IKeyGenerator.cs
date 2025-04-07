using Unscrambler.Constants;

namespace Unscrambler.Derivation;

public interface IKeyGenerator
{
    void Initialize(VersionConstants constants, string? tableBinaryBasePath = null);
    void Generate(Span<byte> initZonePacket);
    
    bool ObfuscationEnabled { get; }
    byte[] Keys { get; }
}
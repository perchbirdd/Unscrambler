using Unscrambler.Constants;

namespace Unscrambler.Unscramble;

public interface IUnscrambler
{
    void Initialize(VersionConstants constants);
    void Unscramble(Span<byte> input, byte key0, byte key1, byte key2);
}
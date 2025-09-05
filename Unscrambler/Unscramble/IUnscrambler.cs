using Unscrambler.Constants;

namespace Unscrambler.Unscramble;

public interface IUnscrambler
{
    /// <summary>
    /// Set the Unscrambler instance to use the provided constants.
    /// </summary>
    /// <param name="constants">The constants to use.</param>
    void Initialize(VersionConstants constants);

    /// <summary>
    /// Unscramble a packet. This method is not supported on game version 2025.07.30.0000.0000 or later.
    /// </summary>
    /// <param name="input">The packet to unscramble, starting at the IPC header.</param>
    /// <param name="key0">Key 0 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key1">Key 1 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key2">Key 2 from PacketDispatcher or an IKeyGenerator.</param>
    /// <exception cref="NotImplementedException">If this IUnscrambler was provided for
    /// version 7.3 / 2025.07.30.0000.0000 or later.</exception>
    void Unscramble(Span<byte> input, byte key0, byte key1, byte key2);

    /// <summary>
    /// Unscramble a packet.
    /// </summary>
    /// <param name="input">The packet to unscramble, starting at the IPC header.</param>
    /// <param name="key0">Key 0 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key1">Key 1 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key2">Key 2 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="opcodeBasedKey">The opcode-based key from an IKeyGenerator.</param>
    void Unscramble(Span<byte> input, byte key0, byte key1, byte key2, int opcodeBasedKey);

    /// <summary>
    /// Unscramble a packet.
    /// </summary>
    /// <param name="input">The packet to unscramble, starting at the IPC header.</param>
    /// <param name="key0">Key 0 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key1">Key 1 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="key2">Key 2 from PacketDispatcher or an IKeyGenerator.</param>
    /// <param name="opcodeKeyTable">The opcodeKeyTable.</param>
    void Unscramble(Span<byte> input, byte key0, byte key1, byte key2, Span<int> opcodeKeyTable);
}
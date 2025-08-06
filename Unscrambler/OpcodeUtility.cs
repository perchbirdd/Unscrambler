namespace Unscrambler;

/// <summary>
/// Functions to make it easier for consumers to work with opcodes.
/// </summary>
public static class OpcodeUtility
{
    private const int PacketHeaderSize = 16;
    private const int OpcodeOffset = 2;

    /// <summary>
    /// Retrieve the opcode from the packet with the buffer starting at the packet header.
    /// </summary>
    /// <param name="packet">The packet buffer, starting at the packet header.</param>
    /// <returns>The opcode of the packet.</returns>
    public static ushort GetOpcodeFromPacketAtPacketStart(Span<byte> packet)
    {
        const int start = PacketHeaderSize + OpcodeOffset;
        const int end = start + 2;
        return BitConverter.ToUInt16(packet[start..end]);
    }

    /// <summary>
    /// Retrieve the opcode from the packet with the buffer starting at the IPC header.
    /// </summary>
    /// <param name="packet">The packet buffer, starting at the IPC header.</param>
    /// <returns>The opcode of the packet.</returns>
    public static ushort GetOpcodeFromPacketAtIpcStart(Span<byte> packet)
    {
        const int start = OpcodeOffset; 
        const int end = start + 2;
        return BitConverter.ToUInt16(packet[start..end]);
    }
}
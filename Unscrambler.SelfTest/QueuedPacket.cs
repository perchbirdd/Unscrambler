using System;

namespace Unscrambler.SelfTest;

public class QueuedPacket
{
    public uint Source { get; set; }
    public int DataSize { get; set; }
    public byte[]? Header { get; set; }
    public ushort Opcode { get; set; }
    public byte[]? GameData { get; set; }
    public string GameDataHash { get; set; }
    
    public byte[]? ScrambledData { get; set; }
    
    public byte[]? UnscramblerData { get; set; }
    public string UnscramblerDataHash { get; set; }
    
    public byte[]? UnscramblerData2 { get; set; }
    public string UnscramblerDataHash2 { get; set; }

    public QueuedPacket() {}

    public QueuedPacket(uint source, int dataSize, byte[] header, ushort opcode, byte[] gameData)
    {
        Source = source;
        DataSize = dataSize;
        Header = header;
        Opcode = opcode;
        GameData = gameData;
    }

    public QueuedPacket(uint source, int dataSize, Span<byte> header, ushort opcode, Span<byte> data)
    {
        Source = source;
        DataSize = dataSize;
        Header = header.ToArray();
        Opcode = opcode;
        GameData = data.ToArray();
    }
}
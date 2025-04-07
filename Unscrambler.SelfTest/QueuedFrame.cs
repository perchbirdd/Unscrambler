using System.Collections.Generic;

namespace Unscrambler.SelfTest;

public enum Direction
{
    None = 0,
    Rx = 1,
    Tx = 2,
}

public enum Protocol
{
    None = 0,
    Zone = 1,
    Chat = 2,
    Lobby = 3,
}

public class QueuedFrame
{
    public Protocol Protocol { get; set; }
    public Direction Direction { get; set; }
    
    public byte[]? Header { get; set; }
    public List<QueuedPacket> Packets { get; init; }

    public QueuedFrame()
    {
        Packets = [];
    }

    public QueuedFrame(byte[] header)
    {
        Header = header;
        Packets = [];
    }
}
using System.IO;

namespace Unscrambler.SelfTest.Binary.Packet;

public class Frame {
    public FrameHeader Header;
    public Packet[] Packets;
    
    public Frame(BinaryReader br) {
        Header = new FrameHeader(br);
        Packets = new Packet[Header.Count];
        for (int i = 0; i < Packets.Length; i++)
            Packets[i] = new Packet(br);
    }
}
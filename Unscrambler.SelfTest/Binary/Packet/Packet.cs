using System.IO;

namespace Unscrambler.SelfTest.Binary.Packet;

public class Packet {
    public PacketElementHeader Header;
    public PacketIpcHeader? IpcHeader;
    public byte[] Data;
    
    public Packet() {}

    public Packet(BinaryReader br) {
        Header = new PacketElementHeader(br);
    
        var dataSize = Header.Size - 16;
        if (Header.Type == PacketType.Ipc) {
            IpcHeader = new PacketIpcHeader(br);
            dataSize -= 16;
        }
    
        Data = br.ReadBytes((int) dataSize);
        if (Data.Length != dataSize)
            throw new IOException("Failed to read full packet from frame.");
        // br.BaseStream.Position += dataSize;
    }
}
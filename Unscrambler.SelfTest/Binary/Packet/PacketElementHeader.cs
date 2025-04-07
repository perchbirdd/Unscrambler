using System.IO;

namespace Unscrambler.SelfTest.Binary.Packet;

public struct PacketElementHeader
{
	public uint Size;
	public uint SrcEntity;
	public uint DstEntity;
	public PacketType Type;
	public ushort Padding;
	
	public PacketElementHeader(BinaryReader br) {
		Size = br.ReadUInt32();
		SrcEntity = br.ReadUInt32();
		DstEntity = br.ReadUInt32();
		Type = (PacketType)br.ReadUInt16();
		Padding = br.ReadUInt16();
	}
}
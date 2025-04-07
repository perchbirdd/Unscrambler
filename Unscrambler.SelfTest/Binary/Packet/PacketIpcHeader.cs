using System.IO;

namespace Unscrambler.SelfTest.Binary.Packet;

// Segment type 3, IPC
public struct PacketIpcHeader
{
	public ushort Unknown;
	public ushort Type; // Opcode
	public ushort Padding01;
	public ushort ServerId;
	public uint Timestamp;
	public uint Padding02;
	
	public PacketIpcHeader(BinaryReader br) {
		Unknown = br.ReadUInt16();
		Type = br.ReadUInt16();
		Padding01 = br.ReadUInt16();
		ServerId = br.ReadUInt16();
		Timestamp = br.ReadUInt32();
		Padding02 = br.ReadUInt32();
	}
};
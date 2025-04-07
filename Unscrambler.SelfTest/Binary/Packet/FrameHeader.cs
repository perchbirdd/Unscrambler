using System.IO;

namespace Unscrambler.SelfTest.Binary.Packet;

public struct FrameHeader
{
	public unsafe fixed byte Prefix[16];
	// public byte[] Prefix;
	public ulong TimeValue;
	public uint TotalSize;
	public PacketProtocol Protocol;
	public ushort Count;
	public byte Version;
	public CompressionType Compression;
	public ushort Unknown;
	public uint DecompressedLength;
	
	// public FrameHeader(ReadOnlySpan<byte> br)
	// {
	// 	var prefix = br[0..16].ToArray();
	// 	br = br[16..];
	// 	
	// 	// var prefix = br.ReadBytes(16);
	// 	// for (int i = 0; i < prefix.Length; i++)
	// 		// Prefix[i] = prefix[i];
	// 	
	// 	TimeValue = BinaryPrimitives.ReadUInt64LittleEndian(br);
	// 	br = br[16..];
	// 	
	// 	TotalSize = BinaryPrimitives.ReadUInt32LittleEndian(br);
	// 	br = br[4..];
	// 	Protocol = (PacketProtocol)BinaryPrimitives.ReadUInt16LittleEndian(br);
	// 	br = br[2..];
	// 	Count = BinaryPrimitives.ReadUInt16LittleEndian(br);
	// 	br = br[2..];
	// 	Version = br[0];
	// 	br = br[1..];
	// 	Compression = (CompressionType)br[0];
	// 	br = br[1..];
	// 	Unknown = BinaryPrimitives.ReadUInt16LittleEndian(br);
	// 	br = br[2..];
	// 	DecompressedLength = BinaryPrimitives.ReadUInt32LittleEndian(br);
	// 	br = br[4..];
	// }
	
	public FrameHeader(BinaryReader br)
	{
		var prefix = br.ReadBytes(16);
		// for (int i = 0; i < prefix.Length; i++)
		// 	Prefix[i] = prefix[i];
		
		TimeValue = br.ReadUInt64();
		TotalSize = br.ReadUInt32();
		Protocol = (PacketProtocol)br.ReadUInt16();
		Count = br.ReadUInt16();
		Version = br.ReadByte();
		Compression = (CompressionType)br.ReadByte();
		Unknown = br.ReadUInt16();
		DecompressedLength = br.ReadUInt32();
	}
}
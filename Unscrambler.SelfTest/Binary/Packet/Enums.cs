namespace Unscrambler.SelfTest.Binary.Packet;

public enum CompressionType : byte {
	None = 0x0,
	Zlib = 0x1,
	Oodle = 0x2,
}

public enum PacketType : ushort {
	None = 0x0,
	SessionInit = 0x1,
	Unknown_2 = 0x2,
	Ipc = 0x3,
	Unknown_4 = 0x4,
	Unknown_5 = 0x5,
	Unknown_6 = 0x6,
	KeepAlive = 0x7,
	KeepAliveResponse = 0x8,
	EncryptionInit = 0x9,
	Unknown_A = 0xA,
	Unknown_B = 0xB,
}

public enum PacketProtocol : ushort {
	None = 0x0,
	Zone = 0x1,
	Chat = 0x2,
	Lobby = 0x3,
}

public enum Direction
{
	Rx,
	Tx,
}
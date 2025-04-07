using System.IO;
using System.Linq;
using System.Text;

namespace Unscrambler.SelfTest.Binary;

public static class BinaryReaderExtensions {
    public static string ReadAscii(this BinaryReader br, int len) {
        var bytes = br.ReadBytes(len);
        var bytes2 = bytes.Where(b => b != 0).ToArray();
        return Encoding.ASCII.GetString(bytes2);
    }

    public static short PeekInt16(this BinaryReader br)
    {
        var ret = br.ReadInt16();
        br.BaseStream.Position -= 2;
        return ret;
    }

    public static bool IsAtEnd(this BinaryReader br)
    {
        return br.BaseStream.Position == br.BaseStream.Length;
    }
}
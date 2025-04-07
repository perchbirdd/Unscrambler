using System.Text.Json.Serialization;
using PeNet;
using Unscrambler;

public class Program
{
    public static void Main(string[] args)
    {
        var exe = args[0];
        if (!File.Exists(exe))
        {
            Console.WriteLine("File not found.");
            return;
        }
        
        var directory = Directory.GetParent(exe)!.FullName;
        var verFile = Path.Combine(directory, "ffxivgame.ver");
        if (!File.Exists(verFile))
        {
            Console.WriteLine("Version file not found.");
            return;
        }
        
        var version = File.ReadAllText(verFile);
        if (!VersionConstants.Constants.TryGetValue(version, out var table))
        {
            Console.WriteLine($"Config for version {version} not found.");
            return;
        }

        var data = File.ReadAllBytes(args[0]);
        var peFile = new PeFile(data);
        var outDir = args[1];

        var rData = peFile.ImageSectionHeaders!.First(s => s.Name == ".rdata");
        var adjustment = rData.VirtualAddress - rData.PointerToRawData;
        DumpArrays(table, data, adjustment, outDir);
    }

    private static void DumpArrays(VersionConstants table, byte[] data, uint adjustment, string outDir)
    {
        DumpArray(data, table.TableOffsets[0]- (nint)adjustment, table.TableSizes[0], Path.Combine(outDir, "table0.bin"));
        DumpArray(data, table.TableOffsets[1] - (nint)adjustment, table.TableSizes[1], Path.Combine(outDir, "table1.bin"));
        DumpArray(data, table.TableOffsets[2] - (nint)adjustment, table.TableSizes[2], Path.Combine(outDir, "table2.bin"));
        DumpArray(data, table.MidTableOffset - (nint)adjustment, table.MidTableSize, Path.Combine(outDir, "midtable.bin"));
        DumpArray(data, table.DayTableOffset - (nint)adjustment, table.DayTableSize, Path.Combine(outDir, "daytable.bin"));
    }
    
    private static void DumpArray(byte[] data, long offset, int length, string path)
    {
        var f = File.OpenWrite(path);
        for (int i = 0; i < length; i++)
        {
            f.WriteByte(data[offset + i]);
        }
        f.Flush();
        f.Close();
    }
}
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;
using PeNet.Header.Resource;

namespace Unscrambler;

public class KeyGenerator
{
    public bool ObfuscationEnabled { get; set; }
    public byte[] Keys { get; set; }
 
    private readonly VersionConstants _constants;
    private int[] _table0 = [];
    private int[] _table1 = [];
    private int[] _table2 = [];
    private byte[] _midTable = [];
    private byte[] _dayTable = [];

    /// <summary>
    /// Create a new KeyGenerator for a game version with constants provided by the Unscrambler library.
    /// </summary>
    /// <param name="gameVersion">The game version.</param>
    public KeyGenerator(string gameVersion)
    {
        _constants = VersionConstants.Constants[gameVersion];
        Keys = new byte[3];

        LoadTables();
    }

    /// <summary>
    /// Create a new KeyGenerator for a game version by providing your own constants and base path for table binaries.
    /// KeyGenerator requires ObfuscationEnabledMode, TableRadixes, and TableMax to be defined, and expects "table0.bin",
    /// "table1.bin", "table2.bin", "midtable.bin" and "daytable.bin" in the provided path.
    /// </summary>
    /// <param name="constants">The constants to initialize the key generator with.</param>
    /// <param name="tableBinaryBasePath">The path containing table binaries.</param>
    public KeyGenerator(VersionConstants constants, string tableBinaryBasePath)
    {
        _constants = constants;
        Keys = new byte[3];
        
        LoadTables(tableBinaryBasePath);
    }
    
    public static byte[] GetResource(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceStream = assembly.GetManifestResourceStream(name);
        using var ms = new MemoryStream();
        if (resourceStream == null) throw new FileNotFoundException();
        resourceStream.CopyTo(ms);
        return ms.ToArray();
    }

    private void LoadTables(string? basePath = null)
    {
        byte[] tTable0;
        byte[] tTable1;
        byte[] tTable2;
        
        if (basePath == null)
        {
            tTable0 = GetResource($"{_constants.GameVersion}.table0.bin");
            tTable1 = GetResource($"{_constants.GameVersion}.table1.bin");
            tTable2 = GetResource($"{_constants.GameVersion}.table2.bin");
            _midTable = GetResource($"{_constants.GameVersion}.midtable.bin");
            _dayTable = GetResource($"{_constants.GameVersion}.daytable.bin");
        }
        else
        {
            tTable0 = File.ReadAllBytes(Path.Combine(basePath, "table0.bin"));
            tTable1 = File.ReadAllBytes(Path.Combine(basePath, "table1.bin"));
            tTable2 = File.ReadAllBytes(Path.Combine(basePath, "table2.bin"));
            _midTable = File.ReadAllBytes(Path.Combine(basePath, "midtable.bin"));
            _dayTable = File.ReadAllBytes(Path.Combine(basePath, "daytable.bin"));
        }

        _table0 = new int[tTable0.Length / 4];
        _table1 = new int[tTable1.Length / 4];
        _table2 = new int[tTable2.Length / 4];
        
        for (int i = 0; i < tTable0.Length; i += 4)
            _table0[i / 4] = BitConverter.ToInt32(tTable0, i);
        
        for (int i = 0; i < tTable1.Length; i += 4)
            _table1[i / 4] = BitConverter.ToInt32(tTable1, i);
            
        for (int i = 0; i < tTable2.Length; i += 4)
            _table2[i / 4] = BitConverter.ToInt32(tTable2, i);
        
    }

    /// <summary>
    /// Generate packet obfuscation keys from an InitZone packet. 
    /// </summary>
    /// <param name="initZonePacket">The initzone packet, starting from after the packet header, sometimes called
    /// the segment header.</param>
    public void Generate(Span<byte> initZonePacket)
    {
        var mode = initZonePacket[37];
        if (mode != _constants.ObfuscationEnabledMode)
        {
            Keys[0] = 0;
            Keys[1] = 0;
            Keys[2] = 0;
            ObfuscationEnabled = false;
            return;
        }
        ObfuscationEnabled = true;

        var seed1 = initZonePacket[38];
        var seed2 = initZonePacket[39];
        var seed3 = BitConverter.ToUInt32(initZonePacket[40..45]);

        var negSeed1 = (byte)~seed1;
        var negSeed2 = (byte)~seed2;
        var negSeed3 = (uint)~seed3;

        Derive(0, negSeed1, negSeed2, negSeed3);
        Derive(1, negSeed1, negSeed2, negSeed3);
        Derive(2, negSeed1, negSeed2, negSeed3);
    }

    private void Derive(byte set, byte nSeed1, byte nSeed2, uint epoch)
    {
        var midIndex = 8 * (nSeed1 % ((_midTable.Length / 8) - 1));
        var midTableValue = _midTable[4 + midIndex];
        var midValue = BitConverter.ToUInt32(_midTable, midIndex);
        
        var epochDays = 3 * (epoch / 60 / 60 / 24);
        var dayTableIndex = 4 * (epochDays % 37);
        var dayTableValue = _dayTable[dayTableIndex];

        var setRadix = _constants.TableRadixes[set];
        var setMax = _constants.TableMax[set];
        var tableIndex = setRadix * (nSeed2 % setMax) + midValue * nSeed1 % setRadix;
        var setResult = set switch
        {
            0 => _table0[tableIndex],
            1 => _table1[tableIndex],
            2 => _table2[tableIndex],
            _ => 0,
        };
        
        Keys[set] = (byte)(nSeed1 + midTableValue + dayTableValue + setResult);
    }
}
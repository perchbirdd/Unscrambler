using System.Reflection;
using Unscrambler.Constants;

namespace Unscrambler.Derivation.Versions;

public class KeyGenerator74 : IKeyGenerator
{
    public bool ObfuscationEnabled { get; set; }
    public byte[] Keys { get; set; }
 
    private VersionConstants _constants;
    private int[] _table0 = [];
    private int[] _table1 = [];
    private int[] _table2 = [];
    private byte[] _midTable = [];
    private byte[] _dayTable = [];
    private int[] _opcodeKeyTable = [];

    public void Initialize(VersionConstants constants, string? tableBinaryBasePath = null)
    {
        _constants = constants;
        Keys = new byte[3];
        LoadTables(tableBinaryBasePath);
    }
    
    public void Initialize(VersionConstants constants, byte[] table0, byte[] table1, byte[] table2, byte[] midTable, byte[] dayTable, byte[]? opcodeKeyTable = null)
    {
        _constants = constants;
        Keys = new byte[3];
        LoadTables(table0, table1, table2, midTable, dayTable, opcodeKeyTable ?? throw new ArgumentNullException(nameof(opcodeKeyTable)));
    }

    private static byte[] GetResource(string name)
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
        byte[] tOpcodeKeyTable;
        byte[] tMidTable;
        byte[] tDayTable;
        
        if (basePath == null)
        {
            tTable0 = GetResource($"{_constants.GameVersion}.table0.bin");
            tTable1 = GetResource($"{_constants.GameVersion}.table1.bin");
            tTable2 = GetResource($"{_constants.GameVersion}.table2.bin");
            tMidTable = GetResource($"{_constants.GameVersion}.midtable.bin");
            tDayTable = GetResource($"{_constants.GameVersion}.daytable.bin");
            tOpcodeKeyTable = GetResource($"{_constants.GameVersion}.opcodekeytable.bin");
        }
        else
        {
            tTable0 = File.ReadAllBytes(Path.Combine(basePath, "table0.bin"));
            tTable1 = File.ReadAllBytes(Path.Combine(basePath, "table1.bin"));
            tTable2 = File.ReadAllBytes(Path.Combine(basePath, "table2.bin"));
            tMidTable = File.ReadAllBytes(Path.Combine(basePath, "midtable.bin"));
            tDayTable = File.ReadAllBytes(Path.Combine(basePath, "daytable.bin"));
            tOpcodeKeyTable = File.ReadAllBytes(Path.Combine(basePath, "opcodekeytable.bin"));
        }

        LoadTables(tTable0, tTable1, tTable2, tMidTable, tDayTable, tOpcodeKeyTable);
    }
    
    private void LoadTables(byte[] table0, byte[] table1, byte[] table2, byte[] midTable, byte[] dayTable, byte[] opcodeKeyTable)
    {
        _table0 = new int[table0.Length / 4];
        _table1 = new int[table1.Length / 4];
        _table2 = new int[table2.Length / 4];
        _opcodeKeyTable = new int[opcodeKeyTable.Length / 4];
        _midTable = midTable;
        _dayTable = dayTable;
        
        for (int i = 0; i < table0.Length; i += 4)
            _table0[i / 4] = BitConverter.ToInt32(table0, i);
        
        for (int i = 0; i < table1.Length; i += 4)
            _table1[i / 4] = BitConverter.ToInt32(table1, i);
            
        for (int i = 0; i < table2.Length; i += 4)
            _table2[i / 4] = BitConverter.ToInt32(table2, i);

        for (int i = 0; i < opcodeKeyTable.Length; i += 4)
            _opcodeKeyTable[i / 4] = BitConverter.ToInt32(opcodeKeyTable, i);
    }
    
    public void GenerateFromInitZone(Span<byte> initZonePacket)
    {
        throw new NotImplementedException("Initialization from InitZone packets is not possible in 7.4 and later.");
    }
    
    public void GenerateFromUnknownInitializer(Span<byte> unknownPacket)
    {
        var mode = unknownPacket[22];
        if (mode != _constants.ObfuscationEnabledMode)
        {
            Keys[0] = 0;
            Keys[1] = 0;
            Keys[2] = 0;
            ObfuscationEnabled = false;
            return;
        }
        ObfuscationEnabled = true;

        var seed1 = unknownPacket[23];
        var seed2 = unknownPacket[24];
        var seed3 = BitConverter.ToUInt32(unknownPacket[28..32]);

        var negSeed1 = (byte)~seed1;
        var negSeed2 = (byte)~seed2;
        var negSeed3 = (uint)~seed3;

        Keys[0] = Derive(0, negSeed1, negSeed2, negSeed3);
        Keys[1] = Derive(1, negSeed1, negSeed2, negSeed3);
        Keys[2] = Derive(2, negSeed1, negSeed2, negSeed3);
    }

    private byte Derive(byte set, byte nSeed1, byte nSeed2, uint epoch)
    {
        var midIndex = 8 * (nSeed1 % (_midTable.Length / 8));
        var midTableValue = _midTable[4 + midIndex];
        var midValue = BitConverter.ToUInt32(_midTable, midIndex);
        
        var epochDays = 3 * (epoch / 60 / 60 / 24);
        var dayTableIndex = 4 * (epochDays % (_dayTable.Length / 4));
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
        
        return (byte)(nSeed1 + midTableValue + dayTableValue + setResult);
    }
    
    public int GetOpcodeBasedKey(int opcode)
    {
        var baseKey = Keys[opcode % 3];
        var opcodeKeyTableIndex = (opcode + baseKey) % _opcodeKeyTable.Length;
        return _opcodeKeyTable[opcodeKeyTableIndex];
    }
}

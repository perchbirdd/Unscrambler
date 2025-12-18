using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unscrambler.Constants;

namespace Unscrambler.SelfTest;

public class TestDataManager
{
    private string _path;
    private VersionConstants _constants;
    
    private HashSet<int> _opcodesToSave;
    private Dictionary<int, string> _opcodeNames;

    private Guid _initPacketId;
    private string _pathForSession;
    
    public TestDataManager(string path, VersionConstants constants)
    {
        _path = path;
        _constants = constants;
        Directory.CreateDirectory(_path);

        _opcodeNames = _constants.ObfuscatedOpcodes.ToDictionary(op => op.Value, op => op.Key);
        _opcodesToSave = _constants.ObfuscatedOpcodes.Select(op => op.Value).ToHashSet();

        foreach (var file in Directory.EnumerateFiles(_path, "*.dat", SearchOption.AllDirectories))
        {
            var datName = Path.GetFileNameWithoutExtension(file);
            foreach (var (opcode, name) in _opcodeNames)
            {
                if (datName.StartsWith(name)) {
                    _opcodesToSave.Remove(opcode);
                }
            }
        }
    }
    
    public void SaveInitZone(Span<byte> initZonePacket)
    {
        _initPacketId = Guid.NewGuid();
        _pathForSession = Path.Combine(_path, _initPacketId.ToString());
        Directory.CreateDirectory(_pathForSession);
        File.WriteAllBytes(Path.Combine(_pathForSession, $"InitZone-{_initPacketId}.dat"), initZonePacket);
    }
    
    public void SaveUnknownInit(Span<byte> unknownInitPacket)
    {
        _initPacketId = Guid.NewGuid();
        _pathForSession = Path.Combine(_path, _initPacketId.ToString());
        Directory.CreateDirectory(_pathForSession);
        File.WriteAllBytes(Path.Combine(_pathForSession, $"UnknownInit-{_initPacketId}.dat"), unknownInitPacket);
    }

    public void Save(int opcode, byte[] obfuscatedData, byte[] deobfuscatedData)
    {
        if (!_opcodesToSave.Contains(opcode)) return;
        File.WriteAllBytes(Path.Combine(_pathForSession, $"{_opcodeNames[opcode]}-obfuscated.dat"), obfuscatedData);
        File.WriteAllBytes(Path.Combine(_pathForSession, $"{_opcodeNames[opcode]}-deobfuscated.dat"), deobfuscatedData);
        _opcodesToSave.Remove(opcode);
    }
}
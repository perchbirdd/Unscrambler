using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Unscrambler.SelfTest;

public unsafe class PluginState
{
    public PacketDispatcher* Dispatcher { get; set; }

    public bool KeysFromDispatcher { get; set; }
    public bool ObfuscationEnabled { get; set; }
    public byte GeneratedKey1 { get; set; }
    public byte GeneratedKey2 { get; set; }
    public byte GeneratedKey3 { get; set; }
    public int OpcodeBasedKey { get; set; }
    
    public int[] OpcodeKeyTable { get; set; }
    
    public HashSet<int> OpcodesNeeded { get; set; }
    
    public Dictionary<int, int> OpcodeSuccesses { get; init; }
    public Dictionary<int, int> OpcodeFailures { get; init; }
    
    public int TargetingHaters { get; set; }

    public PluginState()
    {
        OpcodesNeeded = [];
        OpcodeSuccesses = [];
        OpcodeFailures = [];
        
        OpcodesNeeded = Plugin.Constants.ObfuscatedOpcodes.Select(op => op.Value).ToHashSet();
        var gameVer = Plugin.Constants.GameVersion;
        var table = ReadResourceFromAssembly($"Unscrambler.dll", $"{gameVer}.opcodekeytable.bin");
        OpcodeKeyTable = new int[table.Length / 4];
        for (int i = 0; i < table.Length; i += 4)
            OpcodeKeyTable[i / 4] = BitConverter.ToInt32(table, i);
    }

    public void MarkOpcode(ushort opcode, bool success)
    {
        if (success)
        {
            var count = OpcodeSuccesses.GetValueOrDefault(opcode, 0);
            OpcodeSuccesses[opcode] = count + 1;
        }
        else
        {
            var count = OpcodeFailures.GetValueOrDefault(opcode, 0);
            OpcodeFailures[opcode] = count + 1;
        }
    }
    
    // This is more reliable than doing it in memory, and we have the opcode table binary in the Unscrambler assembly
    private static byte[] ReadResourceFromAssembly(string assemblyFileName, string resourceName)
    {
        var assemblyPath = Path.Combine(Plugin.PluginInterface.AssemblyLocation.DirectoryName!, assemblyFileName);
        var assembly = Assembly.LoadFrom(assemblyPath);
        
        using var resourceStream = assembly.GetManifestResourceStream(resourceName);
        if (resourceStream == null)
        {
            throw new ArgumentException($"Resource '{resourceName}' not found in assembly '{assemblyFileName}'");
        }
        
        var resourceData = new byte[resourceStream.Length];
        resourceStream.ReadExactly(resourceData);
        return resourceData;
    }

}
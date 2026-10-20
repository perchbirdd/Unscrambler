using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Dalamud.Game;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using Unscrambler.Derivation;
using Unscrambler.Derivation.Versions;

namespace Unscrambler.SelfTest;

public class DeriveTester
{
    private IPluginLog _log;
    private IKeyGenerator _generator;

    private delegate nint DerivePrototype(byte set, byte seed1, byte seed2, uint epoch);
    private DerivePrototype _deriveFunc;

    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F8"; // 7.25x2
    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F0"; // 7.25x3
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 54 03 C3"; // 7.3
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 54"; // 7.31
    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F8"; // 7.35x1
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 54 03 C3"; // 7.38
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 C3 03 44 24 64"; // 7.4
    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F0"; // 7.4x2
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 68 03 C3"; // 7.41
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 64 03 C3"; // 7.41x1
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 64"; // 7.45x1
    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F0"; // 7.51x1
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 64 03 C3"; // 7.51x2
    // private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 64"; // 7.55x1
    // private const string _deriveSig = "48 89 74 24 ?? 57 41 0F B6 F0"; // 7.57
    private const string _deriveSig = "E8 ?? ?? ?? ?? 03 44 24 64 03 C3"; // 7.57x1
    
    public DeriveTester(MultiSigScanner scanner, IPluginLog log)
    {
        _log = log;
        _generator = KeyGeneratorFactory.ForGameVersion(Plugin.GameVersion);

        var derivePtr = scanner.ScanText(_deriveSig);
        _deriveFunc = Marshal.GetDelegateForFunctionPointer<DerivePrototype>(derivePtr);
    }

    public void Run()
    {
        var epoch = (uint)DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        _log.Debug($"[DeriveTest] Begin");
        
        for (byte a = 0; a < 255; a++)
        {
            for (byte b = 0; b < 255; b++)
            {
                for (byte s = 0; s < 3; s++)
                {
                    byte gameResult = 0;
                    byte ourResult = 0;
                    try
                    {
                        gameResult = GameDerive(s, a, b, epoch);
                    }
                    catch (Exception e)
                    {
                        _log.Error($"[DeriveTest] Game failure {s} {a} {b} {epoch}");
                    }
                    
                    try
                    {
                        ourResult = GeneratorDerive(s, a, b, epoch);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e,$"[DeriveTest] Generator failure {s} {a} {b} {epoch}");
                        // return;
                    }

                    if (gameResult != ourResult)
                    {
                        _log.Error($"[DeriveTest] Mismatch {s} {a} {b} {epoch}");
                    }
                }
            }
        }
        
        // Do it again but use random epoch values every time
        for (byte a = 0; a < 255; a++)
        {
            for (byte b = 0; b < 255; b++)
            {
                for (byte s = 0; s < 3; s++)
                {
                    byte gameResult = 0;
                    byte ourResult = 0;
                    epoch = (uint)Random.Shared.NextInt64();
                    try
                    {
                        gameResult = GameDerive(s, a, b, epoch);
                    }
                    catch (Exception e)
                    {
                        _log.Error($"[DeriveTest] Game failure {s} {a} {b} {epoch}");
                    }
                    
                    try
                    {
                        ourResult = GeneratorDerive(s, a, b, epoch);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e,$"[DeriveTest] Generator failure {s} {a} {b} {epoch}");
                        // return;
                    }

                    if (gameResult != ourResult)
                    {
                        _log.Error($"[DeriveTest] Mismatch {s} {a} {b} {epoch}");
                    }
                }
            }
        }
        
        _log.Debug($"[DeriveTest] End");
    }

    private byte GameDerive(byte set, byte seed1, byte seed2, uint epoch)
    {
        return (byte) _deriveFunc(set, seed1, seed2, epoch);
    }

    private byte GeneratorDerive(byte set, byte seed1, byte seed2, uint epoch)
    {
        var deriveMethod = typeof(KeyGenerator74).GetMethod("Derive", BindingFlags.NonPublic | BindingFlags.Instance);
        return (byte) deriveMethod.Invoke(_generator,  parameters: [set, seed1, seed2, epoch]); //set, nseed1, nseed2, epoch
    }
}
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
    
    public DeriveTester(MultiSigScanner scanner, IPluginLog log)
    {
        _log = log;
        _generator = KeyGeneratorFactory.ForGameVersion(Plugin.GameVersion);

        var derivePtr = scanner.ScanText("48 89 74 24 ?? 57 41 0F B6 F8");
        // _deriveHook = _hooks.HookFromAddress<DerivePrototype>(derivePtr, DeriveDetour);
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
        var deriveMethod = typeof(KeyGenerator72).GetMethod("Derive", BindingFlags.NonPublic | BindingFlags.Instance);
        return (byte) deriveMethod.Invoke(_generator,  parameters: [set, seed1, seed2, epoch]); //set, nseed1, nseed2, epoch
    }
}
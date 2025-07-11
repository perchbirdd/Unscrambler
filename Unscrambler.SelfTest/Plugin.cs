﻿using System;
using System.IO;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using Unscrambler.Constants;

namespace Unscrambler.SelfTest;

public sealed unsafe class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IGameInteropProvider Hooks { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;
    [PluginService] internal static IFramework DalamudFramework { get; private set; } = null!;
    [PluginService] internal static IObjectTable ObjectTable { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    
    public static string GameVersion { get; private set; }
    public static VersionConstants Constants { get; private set; }

    private const string CommandName = "/ust";

    private readonly PluginState _state;
    private readonly HateTracker _hateTracker;
    private readonly DeriveTester _deriveTester;
    private readonly CaptureHookManager _captureHookManager;

    private readonly WindowSystem _windowSystem;
    private readonly MainWindow _mainWindow;

    public Plugin()
    {
        Log.Info("Loading.");
        
        var gamePath = Environment.ProcessPath;
        var gameDir = Path.GetDirectoryName(gamePath)!;
        var verPath = Path.Combine(gameDir, "ffxivgame.ver");
        GameVersion = File.ReadAllText(verPath);
        Constants = VersionConstants.ForGameVersion(GameVersion);
        
        _state = new PluginState();
        var callback = Framework.Instance()->NetworkModuleProxy->ReceiverCallback;
        _state.Dispatcher = (PacketDispatcher*) &callback->PacketDispatcher;

        var testDataDir = Path.Combine(PluginInterface.GetPluginConfigDirectory(), GameVersion);
        var testDataManager = new TestDataManager(testDataDir, Constants);

        _hateTracker = new HateTracker(DalamudFramework, ObjectTable, ClientState, _state);
        
        var multiSigScanner = new MultiSigScanner(Log);
        _captureHookManager = new CaptureHookManager(Log, multiSigScanner, _state, Hooks, testDataManager);

        _deriveTester = new DeriveTester(multiSigScanner, Log);
        _mainWindow = new MainWindow(_state, _deriveTester);
        _windowSystem =  new WindowSystem("Unscrambler.SelfTest");
        _windowSystem.AddWindow(_mainWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Unscrambler Self Test: /ust"
        });
        
        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }

    public void Dispose()
    {
        _hateTracker.Dispose();
        _captureHookManager.Dispose();
        _windowSystem.RemoveAllWindows();

        _mainWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        ToggleMainUI();
    }

    private void DrawUI() => _windowSystem.Draw();
    public void ToggleMainUI() => _mainWindow.Toggle();
}

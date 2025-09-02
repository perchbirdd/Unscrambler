using System;
using Dalamud.Plugin.Services;

namespace Unscrambler.SelfTest;

public class HateTracker : IDisposable
{
    private IFramework _framework;
    private IObjectTable _objectTable;
    private IClientState _clientState;
    private PluginState _state;
    
    public HateTracker(IFramework framework, IObjectTable objectTable, IClientState clientState, PluginState state)
    {
        _framework = framework;
        _objectTable = objectTable;
        _clientState = clientState;
        _state = state;

        _framework.Update += UpdateHaters;
    }

    public void Dispose()
    {
        _framework.Update -= UpdateHaters;
    }

    private void UpdateHaters(IFramework framework)
    {
        var localPlayerId = _clientState.LocalPlayer?.GameObjectId;
        if (!localPlayerId.HasValue) return;

        var haters = 0;
        foreach (var gameObject in _objectTable)
        {
            if (gameObject.TargetObjectId == localPlayerId.Value)
            {
                haters++;
            }
        }
        
        _state.TargetingHaters = haters;
        // _state.TargetingHaters = 0;
    }
    
    
}
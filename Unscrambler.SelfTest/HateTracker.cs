using System;
using Dalamud.Plugin.Services;

namespace Unscrambler.SelfTest;

public class HateTracker : IDisposable
{
    private readonly IFramework _framework;
    private readonly IObjectTable _objectTable;
    private readonly PluginState _state;
    
    public HateTracker(IFramework framework, IObjectTable objectTable, PluginState state)
    {
        _framework = framework;
        _objectTable = objectTable;
        _state = state;

        _framework.Update += UpdateHaters;
    }

    public void Dispose()
    {
        _framework.Update -= UpdateHaters;
    }

    private void UpdateHaters(IFramework framework)
    {
        var localPlayerId = _objectTable.LocalPlayer?.GameObjectId;
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
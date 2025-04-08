using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace Unscrambler.SelfTest;

public class MainWindow : Window, IDisposable
{
    private readonly PluginState _state;
    
    public MainWindow(PluginState state)
        : base("Unscrambler Self Test", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        IsOpen = true;

        _state = state;
    }

    public void Dispose()
    {
    }

    public override unsafe void Draw()
    {
        var dispatcher = _state.Dispatcher;
        
        ImGui.TextUnformatted($"game random: {dispatcher->GameRandom}");
        ImGui.TextUnformatted($"last packet random: {dispatcher->LastPacketRandom}");
        
        var sub = _state.Dispatcher->GameRandom + _state.Dispatcher->LastPacketRandom;
        ImGui.TextUnformatted($"key0: {dispatcher->Key0} ({dispatcher->Key0 - sub})");
        ImGui.TextUnformatted($"key1: {dispatcher->Key1} ({dispatcher->Key1 - sub})");
        ImGui.TextUnformatted($"key2: {dispatcher->Key2} ({dispatcher->Key2 - sub})");
        
        ImGui.TextUnformatted($"unk: {dispatcher->Unknown_32}");
        
        ImGui.Separator();
        ImGui.TextUnformatted("state keys:");
        ImGui.TextUnformatted($"are keys from dispatcher? {_state.KeysFromDispatcher}");
        var enabled = _state.ObfuscationEnabled ? "Enabled" : "Disabled";
        ImGui.TextUnformatted($"mode: {enabled}");
        ImGui.TextUnformatted($"key0: {_state.GeneratedKey1}");
        ImGui.TextUnformatted($"key1: {_state.GeneratedKey2}");
        ImGui.TextUnformatted($"key2: {_state.GeneratedKey3}");
        ImGui.Separator();
        if (ImGui.Button("Clear"))
        {
            _state.OpcodeSuccesses.Clear();
            _state.OpcodeFailures.Clear();
        }
        ImGui.Separator();
        
        using (var tb = ImRaii.Table("Opcodes", 3))
        {
            ImGui.TableSetupColumn("Opcode");
            ImGui.TableSetupColumn("Success #");
            ImGui.TableSetupColumn("Failure #");
            ImGui.TableHeadersRow();

            foreach (var opcode in Plugin.Constants.ObfuscatedOpcodes)
            {
                var success = _state.OpcodeSuccesses.GetValueOrDefault(opcode.Value, 0);
                var failure = _state.OpcodeFailures.GetValueOrDefault(opcode.Value, 0);

                ImGui.TableNextColumn();

                if (success > 0 && failure > 0)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.DalamudOrange);
                }
                else if (success > 0)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
                }
                else if (failure > 0)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.DalamudRed);
                }
                else
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.DalamudWhite);
                }
                
                ImGui.TextUnformatted($"{opcode.Key} ({opcode.Value:X})");
                
                ImGui.PopStyleColor();
                ImGui.TableNextColumn();
                ImGui.TextUnformatted($"{success}");
                ImGui.TableNextColumn();
                ImGui.TextUnformatted($"{failure}");
                ImGui.TableNextRow();
            }
        }
    }
}
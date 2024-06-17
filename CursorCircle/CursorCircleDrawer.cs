using System;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace CursorCircle
{
    public class CursorCircleDrawer : IDisposable
    {
        private Plugin Plugin;
        private IClientState ClientState;

        public CursorCircleDrawer(Plugin plugin, IClientState clientState)
        {
            Plugin = plugin;
            ClientState = clientState;
        }

        public void Dispose() { }

        public void Draw()
        {
            if (!Plugin.Configuration.OnlyShowInCombat || (Plugin.Configuration.OnlyShowInCombat && IsPlayerInCombat()))
            {
                DrawCursorCircle();
            }
        }

        private bool IsPlayerInCombat()
        {
            return ClientState.LocalPlayer?.StatusFlags.HasFlag(Dalamud.Game.ClientState.Objects.Enums.StatusFlags.InCombat) ?? false;
        }

        private void DrawCursorCircle()
        {
            var io = ImGui.GetIO();
            var cursorPos = io.MousePos;
            var drawList = ImGui.GetForegroundDrawList(); // Use foreground draw list to draw on top

            // Convert color to uint
            uint color = ImGui.ColorConvertFloat4ToU32(Plugin.Configuration.CircleColor);

            // Draw circle around the cursor
            drawList.AddCircle(cursorPos, Plugin.Configuration.CircleRadius, color, 64, Plugin.Configuration.CircleThickness);
        }
    }
}

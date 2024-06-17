using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Dalamud.Plugin.Services;

namespace CursorCircle.Windows
{
    public class ConfigWindow : Window, IDisposable
    {
        private Plugin Plugin;

        public ConfigWindow(Plugin plugin)
            : base("Cursor Circle Settings")
        {
            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(375, 330),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };

            Plugin = plugin;
        }

        public void Dispose() { }

        public override void Draw()
        {
            // Only show in combat checkbox
            bool onlyShowInCombat = Plugin.Configuration.OnlyShowInCombat;
            if (ImGui.Checkbox("Only show in combat", ref onlyShowInCombat))
            {
                Plugin.Configuration.OnlyShowInCombat = onlyShowInCombat;
                Plugin.Configuration.Save();
            }

            ImGui.Spacing();

            // Thickness input
            ImGui.Text("Circle Thickness");
            float circleThickness = Plugin.Configuration.CircleThickness;
            if (ImGui.SliderFloat("##Thickness", ref circleThickness, 1.0f, 10.0f))
            {
                Plugin.Configuration.CircleThickness = circleThickness;
                Plugin.Configuration.Save();
            }

            ImGui.Spacing();

            // Size input
            ImGui.Text("Circle Size");
            float circleRadius = Plugin.Configuration.CircleRadius;
            if (ImGui.SliderFloat("##Size", ref circleRadius, 10.0f, 100.0f))
            {
                Plugin.Configuration.CircleRadius = circleRadius;
                Plugin.Configuration.Save();
            }

            ImGui.Spacing();

            // Color selector
            ImGui.Text("Circle Color");
            Vector4 circleColor = Plugin.Configuration.CircleColor;
            if (ImGui.ColorEdit4("##Color", ref circleColor))
            {
                Plugin.Configuration.CircleColor = circleColor;
                Plugin.Configuration.Save();
            }
        }
    }
}

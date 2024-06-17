using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace CursorCircle.Windows
{
    public class MainWindow : Window, IDisposable
    {
        private Plugin Plugin;
        private bool drawCircleAroundCursor = false;

        private float circleThickness = 2.0f;
        private float circleRadius = 20.0f;
        private Vector4 circleColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        public MainWindow(Plugin plugin)
            : base("Cursor Circle Window")
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
            ImGui.Text("Cursor Circle Plugin");

            ImGui.Spacing();

            // Cursor button
            if (ImGui.Button("Toggle Cursor Circle"))
            {
                drawCircleAroundCursor = !drawCircleAroundCursor;
            }

            ImGui.Spacing();

            // Thickness input
            ImGui.Text("Circle Thickness");
            ImGui.SliderFloat("##Thickness", ref circleThickness, 1.0f, 10.0f);

            ImGui.Spacing();

            // Size input
            ImGui.Text("Circle Size");
            ImGui.SliderFloat("##Size", ref circleRadius, 10.0f, 100.0f);

            ImGui.Spacing();

            // Color selector
            ImGui.Text("Circle Color");
            ImGui.ColorEdit4("##Color", ref circleColor);

            // Draw a circle around the cursor if the toggle is on
            if (drawCircleAroundCursor)
            {
                DrawCursorCircle();
            }
        }

        private void DrawCursorCircle()
        {
            var io = ImGui.GetIO();
            var cursorPos = io.MousePos;
            var drawList = ImGui.GetForegroundDrawList(); // Use foreground draw list to draw on top

            // Convert color to uint
            uint color = ImGui.ColorConvertFloat4ToU32(circleColor);

            // Draw circle around the cursor
            drawList.AddCircle(cursorPos, circleRadius, color, 64, circleThickness);
        }
    }
}

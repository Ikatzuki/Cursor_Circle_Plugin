using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Numerics;

namespace CursorCircle
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public float CircleThickness { get; set; } = 2.0f;
        public float CircleRadius { get; set; } = 20.0f;
        public Vector4 CircleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public bool OnlyShowInCombat { get; set; } = false;

        // the below exist just to make saving less cumbersome
        [NonSerialized]
        private IDalamudPluginInterface? PluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            PluginInterface = pluginInterface;
        }

        public void Save()
        {
            PluginInterface!.SavePluginConfig(this);
        }
    }
}

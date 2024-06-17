using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using CursorCircle.Windows;
using Dalamud.Plugin.Services;

namespace CursorCircle
{
    public sealed class Plugin : IDalamudPlugin
    {
        private const string CommandName = "/pmycommand";

        private DalamudPluginInterface PluginInterface { get; init; }
        private ICommandManager CommandManager { get; init; }
        private IClientState ClientState { get; init; }
        public Configuration Configuration { get; init; }

        public readonly WindowSystem WindowSystem = new("CursorCircle");
        private ConfigWindow configWindow { get; init; }
        private CursorCircleDrawer CursorCircleDrawer { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] ICommandManager commandManager,
            [RequiredVersion("1.0")] IClientState clientState)
        {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;
            ClientState = clientState;

            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginInterface);

            configWindow = new ConfigWindow(this);
            CursorCircleDrawer = new CursorCircleDrawer(this, clientState);

            WindowSystem.AddWindow(configWindow);

            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Open cursor circle settings"
            });

            PluginInterface.UiBuilder.Draw += DrawUI;
            PluginInterface.UiBuilder.Draw += CursorCircleDrawer.Draw;

            // Adds a button that toggles the display status of the main UI of the plugin
            PluginInterface.UiBuilder.OpenConfigUi += ToggleMainUI;
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();

            configWindow.Dispose();
            CursorCircleDrawer.Dispose();

            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            // in response to the slash command, just toggle the display status of our main UI
            ToggleMainUI();
        }

        private void DrawUI() => WindowSystem.Draw();

        public void ToggleMainUI() => configWindow.Toggle();
    }
}

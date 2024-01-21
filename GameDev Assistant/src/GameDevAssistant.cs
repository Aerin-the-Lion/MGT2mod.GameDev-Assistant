using BepInEx;
using GameDevAssistant.Config;
using HarmonyLib;
using BepInEx.Logging;
using System.Diagnostics;

namespace GameDevAssistant
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("Mad Games Tycoon 2.exe")]
    internal class GameDevAssistant : BaseUnityPlugin
    {
        public const string PluginGuid = "me.Aerin.MGT2mod.GameDevAssistant";
        public const string PluginName = "GameDev Assistant";
        public const string PluginVersion = "1.0.1.0";

        void Awake()
        {
            ConfigManager configManager = new ConfigManager(Config);
            LoadHooks();
        }

        void LoadHooks()
        {
            Logger.LogInfo(nameof(LoadHooks));
            Harmony.CreateAndPatchAll(typeof(Hooks), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnTheme), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnGenre), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnAgeTargetGroup), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnAutoDesignSlider), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnStart), PluginGuid);
            Harmony.CreateAndPatchAll(typeof(Hooks.OnTooltipPointerEnter), PluginGuid);
        }
    }
}

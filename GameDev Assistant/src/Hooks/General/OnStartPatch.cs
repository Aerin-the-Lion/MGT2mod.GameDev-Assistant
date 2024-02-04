using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevAssistant.Config;
using GameDevAssistant.Modules;
using GameDevAssistant.Modules.AssistButton;
using GameDevAssistant.Modules.PlatformHistory;
using HarmonyLib;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnStartPatch
        {
            private static void Init()
            {
                AssistButtonManager.Init();
                PlatformsButtonManager.Init();
                LicenceButtonManager.Init();
                SelectButtonManager.Init();
            }
            [HarmonyPostfix, HarmonyLib.HarmonyPatch(typeof(savegameScript), "LoadTasks")]
            private static void Postfix()
            {
                Init();
            }
            [HarmonyPostfix]
            [HarmonyLib.HarmonyPatch(typeof(Menu_NewGame), "OnEnable")]
            private static void Postfix2()
            {
                Init();
            }
        }
    }
}

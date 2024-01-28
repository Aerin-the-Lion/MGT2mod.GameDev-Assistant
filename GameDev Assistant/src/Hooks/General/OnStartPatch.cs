using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevAssistant.Config;
using GameDevAssistant.Modules;
using GameDevAssistant.Modules.PlatformHistory;
using HarmonyLib;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnStartPatch
        {
            [HarmonyPostfix, HarmonyLib.HarmonyPatch(typeof(savegameScript), "LoadTasks")]
            public static void Postfix()
            {
                AssistButtonManager.Init();
                SelectButtonManager.Init();
            }
            [HarmonyPostfix]
            [HarmonyLib.HarmonyPatch(typeof(Menu_NewGame), "OnEnable")]
            public static void Postfix2()
            {
                AssistButtonManager.Init();
                SelectButtonManager.Init();
            }
        }
    }
}

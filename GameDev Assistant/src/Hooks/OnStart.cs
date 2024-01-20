using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevAssistant.Config;
using GameDevAssistant.Modules;
using HarmonyLib;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnStart
        {
            [HarmonyPostfix, HarmonyLib.HarmonyPatch(typeof(savegameScript), "LoadTasks")]
            public static void Postfix()
            {
                AssistManager.Init();
            }
            [HarmonyPostfix]
            [HarmonyLib.HarmonyPatch(typeof(Menu_NewGame), "OnEnable")]
            public static void Postfix2()
            {
                AssistManager.Init();
            }
        }
    }
}

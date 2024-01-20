using GameDevAssistant.Modules;
using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnTooltipPointerEnter
        {
            [HarmonyLib.HarmonyPostfix]
            [HarmonyLib.HarmonyPatch(typeof(tooltip), "OnPointerEnter")]
            public static void Postfix(tooltip __instance, GUI_Tooltip ___guiTooltip)
            {
                //こうしないと名前が変わらない
                if (__instance.name == AssistButtonHandler.buttonName)
                {
                    ___guiTooltip.SetActive(AssistButtonHandler.buttonTooltip);
                }
            }
        }
    }
}

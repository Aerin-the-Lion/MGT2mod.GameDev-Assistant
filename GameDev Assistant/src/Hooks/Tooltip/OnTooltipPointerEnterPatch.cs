using GameDevAssistant.Modules;
using GameDevAssistant.Modules.AssistButton;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnTooltipPointerEnterPatch
        {

            [HarmonyLib.HarmonyPostfix]
            [HarmonyLib.HarmonyPatch(typeof(tooltip), "OnPointerEnter")]
            public static void Postfix(tooltip __instance, GUI_Tooltip ___guiTooltip)
            {
                AssistButtonHandler assistbutton = AssistButtonHandler.Instance;
                //こうしないと名前が変わらない
                if (__instance.name == assistbutton.ButtonName)
                {
                    ___guiTooltip.SetActive(assistbutton.ButtonTooltip);
                }
                PlatformsButtonHandler platformsbutton = PlatformsButtonHandler.Instance;
                if (__instance.name == platformsbutton.ButtonName)
                {
                    ___guiTooltip.SetActive(platformsbutton.ButtonTooltip);
                }
            }
        }
    }
}

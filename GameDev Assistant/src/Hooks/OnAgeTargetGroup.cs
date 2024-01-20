using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using GameDevAssistant.Config;


namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnAgeTargetGroup
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Menu_DevGame), "BUTTON_Zielgruppe")]
            public static void Postfix(Menu_DevGame __instance, genres ___genres_, GUI_Main ___guiMain_)
            {
                if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistAgeTargetEnabled.Value) { return; }

                try
                {
                    Menu_DevGame_Zielgruppe targetMenu =  ___guiMain_.uiObjects[60].GetComponent<Menu_DevGame_Zielgruppe>();
                    for (int i = 0; i < 5; i++)
                    {
                        if (___genres_.IsTargetGroup(__instance.g_GameMainGenre, i))
                        {
                            targetMenu.uiObjects[i + 1].GetComponent<Image>().color = ConfigManager.ColorGood;
                        }
                        else
                        {
                            targetMenu.uiObjects[i + 1].GetComponent<Image>().color = ConfigManager.ColorNormal;
                        }
                    }
                    int target = __instance.g_GameZielgruppe;
                    if(target == 0 || target == -1) { return; }
                    if (___genres_.IsTargetGroup(__instance.g_GameMainGenre, target))
                    {
                        targetMenu.uiObjects[target + 1].GetComponent<Image>().color = ConfigManager.ColorGoodSelected;
                    }
                    else
                    {
                        targetMenu.uiObjects[target + 1].GetComponent<Image>().color = ConfigManager.ColorNormalSelected;
                    }

                }catch(System.Exception e)
                {
                    Debug.LogWarning(nameof(OnAgeTargetGroup) + " : " + e);
                    Debug.LogAssertion(e.StackTrace);
                }
            }
        }
    }
}

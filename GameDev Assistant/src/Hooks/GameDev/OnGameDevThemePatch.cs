using HarmonyLib;
using UnityEngine;
using GameDevAssistant.Config;
using UnityEngine.UI;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnGameDevThemePatch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Menu_DevGame_Theme), "Init")]
            [HarmonyPatch(typeof(Menu_DevGame_Theme), "BUTTON_Search")]
            public static void ColoringTheme(Menu_DevGame_Theme __instance, themes ___themes_, Menu_DevGame ___mDevGame_)
            {
                if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) { return; }

                //Find the theme that fits with the genre, from the list of themes(Content)
                foreach (Transform child in __instance.uiObjects[0].transform)
                {
                    GameObject childObj = child.gameObject;
                    Item_DevGame_Theme myTheme = childObj.GetComponent<Item_DevGame_Theme>();
                    if (___themes_.IsThemesFitWithGenre(myTheme.myID, ___mDevGame_.g_GameMainGenre))
                    {
                        myTheme.GetComponent<Image>().color = ConfigManager.ColorGood;
                    }
                    else
                    {
                        myTheme.GetComponent<Image>().color = ConfigManager.ColorNormal;
                    }
                }
            }
        }
    }
}

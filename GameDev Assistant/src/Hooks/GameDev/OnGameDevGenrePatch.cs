
using UnityEngine;
using HarmonyLib;
using GameDevAssistant.Config;
using UnityEngine.UI;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnGameDevGenrePatch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Menu_DevGame_Genre), "Init")]
            public static void ColoringGenre(Menu_DevGame_Genre __instance, int g, genres ___genres_, Menu_DevGame ___mDevGame_)
            {
                if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value || g != 1){return;}

                foreach (Transform child in __instance.uiObjects[0].transform)
                {
                    GameObject childObj = child.gameObject;
                    Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();
                    if(___genres_.IsGenreCombination(___mDevGame_.g_GameMainGenre, myGenre.myID))
                    {
                        myGenre.GetComponent<Image>().color = ConfigManager.ColorGood;
                    }
                    else
                    {
                        myGenre.GetComponent<Image>().color = ConfigManager.ColorNormal;
                    }
                }
            }
        }
    }
}
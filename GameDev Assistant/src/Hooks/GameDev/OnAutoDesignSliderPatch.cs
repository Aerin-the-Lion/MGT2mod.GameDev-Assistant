using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using GameDevAssistant.Config;

// note with English translation:
// Designschwerpunkt = Design Focus
// Designausrichtung = Design Direction


namespace GameDevAssistant
{
    public partial class Hooks
    {
        public class OnAutoDesignSlider
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Menu_DevGame), "BUTTON_AutoDesignSettings")]
            public static void AlwaysPerfectSlider(Menu_DevGame __instance, genres ___genres_, int ___g_GameMainGenre, int ___g_GameSubGenre)
            {
                if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistAutoDesignSliderEnabled.Value) { return; }

                // Find the design focus and design direction that fits with the genre, from the list of design focus and design direction
                // without a review data.

                UpdateDesignFocus(__instance, ___genres_, ___g_GameMainGenre, ___g_GameSubGenre);
                UpdateDesignDirection(__instance, ___genres_, ___g_GameMainGenre, ___g_GameSubGenre);
                SetWorkPriorities(__instance, ___genres_, ___g_GameMainGenre);
                __instance.UpdateDesignSettings();
                __instance.UpdateDesignSlider();

            }
            private static void UpdateDesignFocus(Menu_DevGame instance, genres genresData, int mainGenre, int subGenre)
            {
                for (int i = 0; i < instance.g_Designschwerpunkt.Length; i++)
                {
                    instance.g_Designschwerpunkt[i] = genresData.GetFocus(i, mainGenre, subGenre);
                }
            }

            private static void UpdateDesignDirection(Menu_DevGame instance, genres genresData, int mainGenre, int subGenre)
            {
                for (int j = 0; j < instance.g_Designausrichtung.Length; j++)
                {
                    instance.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = instance.g_Designausrichtung[j].ToString();
                    instance.g_Designausrichtung[j] = genresData.GetAlign(j, mainGenre, subGenre);
                }
            }

            private static void SetWorkPriorities(Menu_DevGame instance, genres genresData, int mainGenre)
            {
                // Find with a data of MGT2
                instance.uiObjects[97].GetComponent<Slider>().value = genresData.genres_GAMEPLAY[mainGenre] / 5f;
                instance.uiObjects[98].GetComponent<Slider>().value = genresData.genres_GRAPHIC[mainGenre] / 5f;
                instance.uiObjects[99].GetComponent<Slider>().value = genresData.genres_SOUND[mainGenre] / 5f;
                instance.uiObjects[100].GetComponent<Slider>().value = genresData.genres_CONTROL[mainGenre] / 5f;
                instance.SetAP_Gameplay();
                instance.SetAP_Grafik();
                instance.SetAP_Sound();
                instance.SetAP_Technik();
            }
        }
    }
}


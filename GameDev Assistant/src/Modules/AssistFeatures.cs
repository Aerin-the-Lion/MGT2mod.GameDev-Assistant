using HarmonyLib;
using UnityEngine;
using GameDevAssistant.Config;
using System.Collections.Generic;

namespace GameDevAssistant.Modules
{
    public class AssistFeatures: MonoBehaviour
    {
        public static GameObject obj_Dev_Game;
        public static Menu_DevGame menu_Dev_Game;
        public static genres genres_;
        public static themes themes_;
        public static GUI_Main guiMain_;

        public static void FindScripts()
        {
            if(obj_Dev_Game == null)
            {
                obj_Dev_Game = GameObject.Find("CanvasInGameMenu").transform.Find("Menu_Dev_Game").gameObject;
            }
            if(menu_Dev_Game == null)
            {
                menu_Dev_Game = obj_Dev_Game.GetComponent<Menu_DevGame>();
            }
            if(genres_ == null)
            {
                genres_ = Traverse.Create(menu_Dev_Game).Field("genres_").GetValue<genres>();
            }
            if(themes_ == null)
            {
                themes_ = Traverse.Create(menu_Dev_Game).Field("themes_").GetValue<themes>();
            }
            if(guiMain_ == null)
            {
                guiMain_ = Traverse.Create(menu_Dev_Game).Field("guiMain_").GetValue<GUI_Main>();
            }
        }

        public static void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }
            FindScripts();

            //名前自動化
            if (ConfigManager.IsAssistRandomNameEnabled.Value)
            {
                menu_Dev_Game.BUTTON_RandomGameName();
            }

            //ジャンル自動化
            FindFitMainGenreAtRandom();
            FindFitSubGenreAtRandom();

            //テーマ自動化
            FindFitMainThemeAtRandom();
            FindFitSubThemeAtRandom();

            //ターゲット年齢自動化
            FindFitAgeTargetGroupAtRandom();

            //デザイン設定自動化
            if (ConfigManager.IsAssistAutoDesignSliderEnabled.Value)
            {
                menu_Dev_Game.BUTTON_AutoDesignSettings();
            }
        }


        public static void FindFitMainGenreAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            Menu_DevGame_Genre menuGenre = guiMain_.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();

            //まず最初に、該当のFitするジャンルを探し、それをリストに追加する
            foreach (Transform child in menuGenre.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();

                if (myGenre != null && genres_.IsGenreCombination(menu_Dev_Game.g_GameMainGenre, myGenre.myID))
                {
                    validGenres.Add(myGenre);
                }else if(menu_Dev_Game.g_GameMainGenre <= 0) 　//もしメインジャンルが未設定の場合は、全てのジャンルをリストに追加する
                {
                    validGenres.Add(myGenre);
                }
            }
            
            //該当のジャンルが見つかった場合、その中からランダムに選択する
            if (validGenres.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validGenres.Count);
                Item_DevGame_Genre selectedGenre = validGenres[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                menu_Dev_Game.SetMainGenre(selectedGenre.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
        }

        public static void FindFitSubGenreAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            Menu_DevGame_Genre menuGenre = guiMain_.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();

            foreach (Transform child in menuGenre.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();

                if (myGenre != null && genres_.IsGenreCombination(menu_Dev_Game.g_GameMainGenre, myGenre.myID))
                {
                    validGenres.Add(myGenre);
                }
                else if (menu_Dev_Game.g_GameMainGenre <= 0)
                {
                    validGenres.Add(myGenre);
                }
            }

            if (validGenres.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validGenres.Count);
                Item_DevGame_Genre selectedGenre = validGenres[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                menu_Dev_Game.SetSubGenre(selectedGenre.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
        }

        public static void FindFitMainThemeAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            Menu_DevGame_Theme menuTheme = guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>();
            menuTheme.Init(0);
            menuTheme.gameObject.SetActive(true); //検索結果がバグるので、これで対策。

            List<Item_DevGame_Theme> validTheme = new List<Item_DevGame_Theme>();

            foreach (Transform child in menuTheme.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Theme myTheme = childObj.GetComponent<Item_DevGame_Theme>();

                if (myTheme != null && themes_.IsThemesFitWithGenre(myTheme.myID, menu_Dev_Game.g_GameMainGenre))
                {
                    validTheme.Add(myTheme);
                }
                else if (menu_Dev_Game.g_GameMainTheme <= 0)
                {
                    validTheme.Add(myTheme);
                }
            }

            if (validTheme.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validTheme.Count);
                Item_DevGame_Theme selectedTheme = validTheme[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                menu_Dev_Game.SetMainTheme(selectedTheme.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
            menuTheme.gameObject.SetActive(false);//検索結果がバグるので、これで対策。
        }
        public static void FindFitSubThemeAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            Menu_DevGame_Theme menuTheme = guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>();

            List<Item_DevGame_Theme> validTheme = new List<Item_DevGame_Theme>();

            foreach (Transform child in menuTheme.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Theme myTheme = childObj.GetComponent<Item_DevGame_Theme>();

                if (myTheme != null && themes_.IsThemesFitWithGenre(myTheme.myID, menu_Dev_Game.g_GameMainGenre))
                {
                    validTheme.Add(myTheme);
                }
                else if (menu_Dev_Game.g_GameMainTheme <= 0)
                {
                    validTheme.Add(myTheme);
                }
            }

            if (validTheme.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validTheme.Count);
                Item_DevGame_Theme selectedTheme = validTheme[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                menu_Dev_Game.SetSubTheme(selectedTheme.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
        }

        public static void FindFitAgeTargetGroupAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistAgeTargetEnabled.Value) return;

            Menu_DevGame_Zielgruppe menuAge = guiMain_.uiObjects[60].GetComponent<Menu_DevGame_Zielgruppe>();


            List<int> validAgeIndices = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                if (genres_.IsTargetGroup(menu_Dev_Game.g_GameMainGenre, i))
                {
                    validAgeIndices.Add(i);
                }
            }

            if (validAgeIndices.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validAgeIndices.Count);
                int selectedAgeIndex = validAgeIndices[randomIndex];
                menu_Dev_Game.SetZielgruppe(selectedAgeIndex); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となる年齢グループが見つからなかった場合の処理
            }
        }
    }
}

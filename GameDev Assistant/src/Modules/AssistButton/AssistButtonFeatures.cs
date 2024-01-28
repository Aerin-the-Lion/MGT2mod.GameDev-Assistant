using HarmonyLib;
using UnityEngine;
using GameDevAssistant.Config;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace GameDevAssistant.Modules
{
    public partial class AssistButtonFeatures: MonoBehaviour
    {
        private GameObject _obj_Dev_Game;
        private Menu_DevGame _menu_Dev_Game;
        private genres _genres;
        private themes _themes;
        private GUI_Main _guiMain;

        private static AssistButtonFeatures _instance { get; set; }

        public static AssistButtonFeatures Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssistButtonFeatures();
                }
                return _instance;
            }
        }

        private void FindScripts()
        {
            if(_obj_Dev_Game == null)
            {
                _obj_Dev_Game = GameObject.Find("CanvasInGameMenu").transform.Find("Menu_Dev_Game").gameObject;
            }
            if(_menu_Dev_Game == null)
            {
                _menu_Dev_Game = _obj_Dev_Game.GetComponent<Menu_DevGame>();
            }
            if(_genres == null)
            {
                _genres = Traverse.Create(_menu_Dev_Game).Field("genres_").GetValue<genres>();
            }
            if(_themes == null)
            {
                _themes = Traverse.Create(_menu_Dev_Game).Field("themes_").GetValue<themes>();
            }
            if(_guiMain == null)
            {
                _guiMain = Traverse.Create(_menu_Dev_Game).Field("guiMain_").GetValue<GUI_Main>();
            }
        }

        public void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }
            FindScripts();
            _isInitPlatform = false;

            //名前自動化
            if (ConfigManager.IsAssistRandomNameEnabled.Value)
            {
                _menu_Dev_Game.BUTTON_RandomGameName();
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
                _menu_Dev_Game.BUTTON_AutoDesignSettings();
            }

            //プラットフォーム自動化
            for(int i = 0; i < 3; i++)
            {
                InitializePlatformSelection(i, false);
            }
        }


        private void FindFitMainGenreAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            if (ConfigManager.IsPinnedMainGenreEnabled.Value && _menu_Dev_Game.g_GameMainGenre > 0) return;
            Menu_DevGame_Genre menuGenre = _guiMain.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();

            //まず最初に、該当のFitするジャンルを探し、それをリストに追加する
            foreach (Transform child in menuGenre.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();

                if (myGenre != null) //もしメインジャンルが未設定の場合は、全てのジャンルをリストに追加する
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
                _menu_Dev_Game.SetMainGenre(selectedGenre.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
        }

        private void FindFitSubGenreAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            if (!IsSubGenreUnlocked(_menu_Dev_Game)) { return; }

            Menu_DevGame_Genre menuGenre = _guiMain.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();
            int mainGenre = _menu_Dev_Game.g_GameMainGenre;

            foreach (Transform child in menuGenre.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();
                if (myGenre == null) { continue; }
                if(myGenre.myID == mainGenre){ continue; }

                if (myGenre != null && _genres.IsGenreCombination(_menu_Dev_Game.g_GameMainGenre, myGenre.myID))
                {
                    validGenres.Add(myGenre);
                }
                else if (_menu_Dev_Game.g_GameMainGenre <= 0)
                {
                    validGenres.Add(myGenre);
                }
            }

            if (validGenres.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validGenres.Count);
                Item_DevGame_Genre selectedGenre = validGenres[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                _menu_Dev_Game.SetSubGenre(selectedGenre.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
        }

        private void FindFitMainThemeAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            Menu_DevGame_Theme menuTheme = _guiMain.uiObjects[62].GetComponent<Menu_DevGame_Theme>();
            menuTheme.Init(0);
            menuTheme.gameObject.SetActive(true); //検索結果がバグるので、これで対策。

            List<Item_DevGame_Theme> validTheme = new List<Item_DevGame_Theme>();

            foreach (Transform child in menuTheme.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Theme myTheme = childObj.GetComponent<Item_DevGame_Theme>();

                if (myTheme != null && _themes.IsThemesFitWithGenre(myTheme.myID, _menu_Dev_Game.g_GameMainGenre))
                {
                    validTheme.Add(myTheme);
                }
                else if (_menu_Dev_Game.g_GameMainTheme <= 0)
                {
                    validTheme.Add(myTheme);
                }
            }

            if (validTheme.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validTheme.Count);
                Item_DevGame_Theme selectedTheme = validTheme[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                _menu_Dev_Game.SetMainTheme(selectedTheme.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
            menuTheme.gameObject.SetActive(false);//検索結果がバグるので、これで対策。
        }
        private void FindFitSubThemeAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            if (!IsSubThemeUnlocked(_menu_Dev_Game)) { return; }

            Menu_DevGame_Theme menuTheme = _guiMain.uiObjects[62].GetComponent<Menu_DevGame_Theme>();
            menuTheme.Init(0);
            menuTheme.gameObject.SetActive(true); //検索結果がバグるので、これで対策。

            List<Item_DevGame_Theme> validTheme = new List<Item_DevGame_Theme>();
            int mainTheme = _menu_Dev_Game.g_GameMainTheme;

            foreach (Transform child in menuTheme.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Theme myTheme = childObj.GetComponent<Item_DevGame_Theme>();
                if (myTheme == null) { continue; }
                if (myTheme.myID == mainTheme) { continue; }

                if (myTheme != null && _themes.IsThemesFitWithGenre(myTheme.myID, _menu_Dev_Game.g_GameMainGenre))
                {
                    validTheme.Add(myTheme);
                }
                else if (_menu_Dev_Game.g_GameMainTheme <= 0)
                {
                    validTheme.Add(myTheme);
                }
            }

            if (validTheme.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validTheme.Count);
                Item_DevGame_Theme selectedTheme = validTheme[randomIndex];
                // ここでselectedGenreを使用して何か処理を行う
                _menu_Dev_Game.SetSubTheme(selectedTheme.myID); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となるジャンルが見つからなかった場合の処理
            }
            menuTheme.gameObject.SetActive(false);//検索結果がバグるので、これで対策。
        }

        private void FindFitAgeTargetGroupAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistAgeTargetEnabled.Value) return;

            Menu_DevGame_Zielgruppe menuAge = _guiMain.uiObjects[60].GetComponent<Menu_DevGame_Zielgruppe>();


            List<int> validAgeIndices = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                if (_genres.IsTargetGroup(_menu_Dev_Game.g_GameMainGenre, i))
                {
                    validAgeIndices.Add(i);
                }
            }

            if (validAgeIndices.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validAgeIndices.Count);
                int selectedAgeIndex = validAgeIndices[randomIndex];
                _menu_Dev_Game.SetZielgruppe(selectedAgeIndex); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となる年齢グループが見つからなかった場合の処理
            }
        }
        //forschungSonstiges - Research Miscellaneous

        private bool IsSubGenreUnlocked(Menu_DevGame menu) 
        {
            //forschungSonstiges_
            forschungSonstiges resMis = Traverse.Create(menu).Field("forschungSonstiges_").GetValue<forschungSonstiges>();
            if (resMis != null && resMis.IsErforscht(35))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsSubThemeUnlocked(Menu_DevGame menu)
        {
            //forschungSonstiges_
            forschungSonstiges resMis = Traverse.Create(menu).Field("forschungSonstiges_").GetValue<forschungSonstiges>();
            if (resMis != null && resMis.IsErforscht(36))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

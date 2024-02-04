using GameDevAssistant.Config;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevAssistant.Modules.AssistButton
{
    public partial class AssistButtonFeatures
    {
        private const string BUTTON_MAIN_THEME = "Button_MainTheme";
        private const string BUTTON_SUB_THEME = "Button_SubTheme";

        private void SetFitMainThemeAtRandom()
        {
            // Early return
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_MAIN_THEME);
            if (!isButtonInteractable) { return; }
            // ---------------------------------------------------------------------------------------
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
        private void SetFitSubThemeAtRandom()
        {
            // Early return
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistThemeEnabled.Value) return;
            if (!IsSubThemeUnlocked(_menu_Dev_Game)) { return; }
            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_SUB_THEME);
            if (!isButtonInteractable) { return; }
            // ---------------------------------------------------------------------------------------
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

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
        public const string BUTTON_MAIN_GENRE = "Button_MainGenre";
        public const string BUTTON_SUB_GENRE = "Button_SubGenre";

        private void SetFitMainGenreAtRandom()
        {
            // Early returns
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            if (ConfigManager.IsPinnedMainGenreEnabled.Value && _menu_Dev_Game.g_GameMainGenre > 0) return;
            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_MAIN_GENRE);
            if (!isButtonInteractable) { return; }

            // -------------------------------------------------------------------------------------

            //get the genre menu from the game
            Menu_DevGame_Genre menuGenre = _guiMain.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();
            //まず最初に、該当のFitするジャンルを探し、それをリストに追加する
            //First, find the corresponding genre that fits and add it to the list
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

        private void SetFitSubGenreAtRandom()
        {
            // Early returns
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistGenreEnabled.Value) return;
            if (!IsSubGenreUnlocked(_menu_Dev_Game)) { return; }
            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_SUB_GENRE);
            if (!isButtonInteractable) { return; }
            // -------------------------------------------------------------------------------------

            Menu_DevGame_Genre menuGenre = _guiMain.uiObjects[61].GetComponent<Menu_DevGame_Genre>();
            menuGenre.Init(0);

            List<Item_DevGame_Genre> validGenres = new List<Item_DevGame_Genre>();
            int mainGenre = _menu_Dev_Game.g_GameMainGenre;

            foreach (Transform child in menuGenre.uiObjects[0].transform)
            {
                GameObject childObj = child.gameObject;
                Item_DevGame_Genre myGenre = childObj.GetComponent<Item_DevGame_Genre>();
                if (myGenre == null) { continue; }
                if (myGenre.myID == mainGenre) { continue; }

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
    }
}

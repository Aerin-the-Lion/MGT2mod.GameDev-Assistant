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
        public const string BUTTON_LICENCE = "Button_Lizenz";

        private void InitLicence(Menu_GameDev_Licence menu)
        {
            Traverse.Create(menu).Method("FindScripts").GetValue();
            menu.InitDropdowns();
            Traverse.Create(menu).Method("Init").GetValue();
            SetLicenceFilter(menu, ConfigManager.LicenceFilter);
            menu.InitDropdowns();
        }

        /*
        private void SetLicence()
        {
            // Early return
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistLicenseEnabled.Value) return;
            //check if the button is interactable
            bool isButtonInteractable = Helper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_LICENCE);
            if (!isButtonInteractable) { return; }

            //get the genre menu from the game
            Menu_GameDev_Licence menuLicence = _guiMain.uiObjects[61].GetComponent<Menu_GameDev_Licence>();
            InitLicence(menuLicence);

            List<Menu_GameDev_Licence> validGenres = new List<Menu_GameDev_Licence>();
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

        */
        /// <summary>
        /// Licence用のアシスト機能を追加する
        /// </summary>
        /// <param name="randomSelection"></param>
        public void InitializeLicenceSelection(bool randomSelection = false)
        {
            if (!IsLicenceSelectionEnabled()) return;

            //各スロットがアンロックされているかどうかを確認する
            //bool[] isUnlocked = IsSlotsUnlocked();
            //if (!isUnlocked[platformSlot]) { return; }

            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_LICENCE);
            if (!isButtonInteractable) { return; }

            var menuLicence = GetMenuLicence();
            if (menuLicence == null) return;
            var validLicence = FindValidLicences(menuLicence);

            if (validLicence.Any())
            {
                var selectedLicence = randomSelection ? ChooseRandomLicence(validLicence) : validLicence.First();
                _menu_Dev_Game.SetLicence(selectedLicence.myID);
            }
            else
            {
                // 対象となるプラットフォームが見つからなかった場合の処理
            }
            _sfx.PlaySound(3, false);
        }

        private bool IsLicenceSelectionEnabled() => ConfigManager.IsModEnabled.Value;

        private Menu_GameDev_Licence GetMenuLicence()
        {
            var menuLicence = _guiMain.uiObjects[63].GetComponent<Menu_GameDev_Licence>();
            InitLicence(menuLicence);
            return menuLicence;
        }

        private List<Item_DevGame_Licence> FindValidLicences(Menu_GameDev_Licence menuLicence)
        {
            return menuLicence.uiObjects[0].transform
                .Cast<Transform>()
                .Select(child => child.gameObject.GetComponent<Item_DevGame_Licence>())
                .Where(licence => licence != null)
                .ToList();
        }

        private Item_DevGame_Licence ChooseRandomLicence(List<Item_DevGame_Licence> Licence) =>
                Licence[UnityEngine.Random.Range(0, Licence.Count)];

        /// <summary>
        /// ライセンスのフィルターを設定する
        /// in English: Set the licence filter with the specified filter ID
        /// (0 : Name, 1 : Sales price, 2: Popularity...)
        /// </summary>
        /// <param name="menuLicence"></param>
        /// <param name="filterId"></param>
        private void SetLicenceFilter(Menu_GameDev_Licence menuLicence, int filterId)
        {
            //Debug.Log("SetFilter");
            //Debug.Log("filterId : " + filterId);
            menuLicence.uiObjects[1].GetComponent<Dropdown>().value = filterId;
        }

        private void SetLicenceName()
        {
            if (!ConfigManager.IsSetLicenceNameEnabled.Value) { return; }
            _menu_Dev_Game.SetLicenceName();
        }
    }
}

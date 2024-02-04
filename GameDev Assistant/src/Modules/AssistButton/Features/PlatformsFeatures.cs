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
        public const string PLATFORM_BUTTON_NAME = "Button_Platform";

        private bool _isInitPlatform = false;

        /// <summary>
        /// プラットフォーム用のアシスト機能を追加する
        /// </summary>
        /// <param name="platformSlot"></param>
        /// <param name="randomSelection"></param>
        public void InitializePlatformSelection(int platformSlot, bool randomSelection = false)
        {
            if (!IsPlatformSelectionEnabled()) return;

            //各スロットがアンロックされているかどうかを確認する
            bool[] isUnlocked = IsSlotsUnlocked(platformSlot);
            if (!isUnlocked[platformSlot]) { return; }

            //Exclusive Platformの場合、Main Platformのみを選択する
            if(ConfigManager.PlatformType == 1 && platformSlot != 0){ return ;}

            var menuPlatform = GetMenuPlatform(platformSlot);
            if (menuPlatform == null) return;
            var validPlatforms = FindValidPlatforms(menuPlatform);

            if (validPlatforms.Any())
            {
                var selectedPlatform = randomSelection ? ChooseRandomPlatform(validPlatforms) : validPlatforms.First();
                _menu_Dev_Game.SetPlatform(menuPlatform.platformNR, selectedPlatform.myID);
            }
            else
            {
                // 対象となるプラットフォームが見つからなかった場合の処理
            }
           menuPlatform.gameObject.SetActive(false);
        }

        private bool IsPlatformSelectionEnabled() => ConfigManager.IsModEnabled.Value;

        private Menu_DevGame_Platform GetMenuPlatform(int platformSlot)
        {
            var menuPlatform = _guiMain.uiObjects[66].GetComponent<Menu_DevGame_Platform>();
            InitializePlatformMenu(menuPlatform, platformSlot);
            return menuPlatform;
        }

        private void InitializePlatformMenu(Menu_DevGame_Platform menuPlatform, int platformSlot)
        {
            _menu_Dev_Game.g_GamePlatform = InitExistPlatforms(_menu_Dev_Game.g_GamePlatform);
            SetPlatformType(ConfigManager.PlatformType);
            menuPlatform.Init(platformSlot);
            menuPlatform.gameObject.SetActive(true);
            SetPlatformsFilter(menuPlatform, ConfigManager.PlatformFilter);
            menuPlatform.DROPDOWN_Sort();
        }

        private List<Item_DevGame_Platform> FindValidPlatforms(Menu_DevGame_Platform menuPlatform)
        {
            return menuPlatform.uiObjects[0].transform
                .Cast<Transform>()
                .Select(child => child.gameObject.GetComponent<Item_DevGame_Platform>())
                .Where(platform => platform != null && !IsExistSamePlatforms(platform.myID))
                .ToList();
        }

        private Item_DevGame_Platform ChooseRandomPlatform(List<Item_DevGame_Platform> platforms) => 
            platforms[UnityEngine.Random.Range(0, platforms.Count)];

        /*
         * 0 Name
         * 1 Manufacturer
         * 2 Release Date
         * 3 Technology level
         * 4 Purchase price
         * 5 Market share
         * 6 Available Games
         * 7 Development costs
         * 8 Platform type
         * 9 Active users
         */


        /// <summary>
        /// プラットフォームのフィルターを設定する
        /// in English: Set the platform filter with the specified filter ID
        /// (0 : Name, 1 : Manufacturer, 2: Release Date...)
        /// </summary>
        /// <param name="menuPlatform"></param>
        /// <param name="filterId"></param>
        private void SetPlatformsFilter(Menu_DevGame_Platform menuPlatform, int filterId)
        {
            //Debug.Log("SetFilter");
            //Debug.Log("filterId : " + filterId);
            menuPlatform.uiObjects[1].GetComponent<Dropdown>().value = filterId;
        }

        /// <summary>
        /// プラットフォームのタイプフィルターを設定する
        /// in English: Set the platform type filter with the specified type ID
        /// (0 : Multiplatform, 1 : Exclusive, 2: Manufacturer, 3: Retro...)
        /// </summary>
        /// <param name="menuPlatform"></param>
        /// <param name="filterId"></param>
        private void SetPlatformType( int platformTypeId)
        {
            //Debug.Log("SetFilter");
            //Debug.Log("filterId : " + filterId);
            if (_menu_Dev_Game.uiObjects[146].GetComponent<Dropdown>().interactable == false) return;
            if (IsArcadeCabinetGame()) return;
            _menu_Dev_Game.uiObjects[146].GetComponent<Dropdown>().value = platformTypeId;
        }


        /// <summary>
        /// プラットフォームには4つスロットがあるので、それらがアンロックされているかどうかを確認する
        /// 0 = Free Slot(元から開放されている), 1 slot = 28, 2 slot = 29, 3 slot = 30
        /// in english: There are four slots for platforms, so check if they are unlocked.
        /// in english: 0 = Free Slot (unlocked from the beginning), 1 = 28, 2 = 29, 3 = 30
        /// </summary>
        /// <returns></returns>
        private bool[] IsSlotsUnlocked(int slot)
        {
            bool[] isUnlocked = new bool[4];
            forschungSonstiges resMis = Traverse.Create(_menu_Dev_Game).Field("forschungSonstiges_").GetValue<forschungSonstiges>();

            //実際にロックされているかどうかを確認する
            string buttonName = PLATFORM_BUTTON_NAME + (slot + 1).ToString();
            bool isInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, buttonName);

            //元から開放されている。
            if(isInteractable)
            {
                isUnlocked[0] = true;
            }

            if (resMis != null && resMis.IsErforscht(28) && isInteractable)
            {
                isUnlocked[1] = true ;
            }
            if (resMis != null && resMis.IsErforscht(29) && isInteractable)
            {
                isUnlocked[2] = true;
            }
            if (resMis != null && resMis.IsErforscht(30) && isInteractable)
            {
                isUnlocked[3] = true;
            }
            return isUnlocked;
        }

        /// <summary>
        /// プラットフォームを初期化する
        /// in English: Initialize the platform
        /// </summary>
        /// <param name="existedPlatform"></param>
        /// <returns></returns>
        private int[] InitExistPlatforms(int[] existedPlatform)
        {
            if(_isInitPlatform) { return existedPlatform; }

            for (int i = 0; i < existedPlatform.Length; i++)
            {
                existedPlatform[i] = -1;
                _menu_Dev_Game.SetPlatform(i, -1);
            }

            _isInitPlatform = true;
            return existedPlatform;
        }

        /// <summary>
        /// 同じプラットフォームが存在するかどうかを確認する
        /// in English: Check if the same platform exists
        /// </summary>
        /// <param name="platformID"></param>
        /// <returns></returns>
        private bool IsExistSamePlatforms(int platformID)
        {
            int[] existedPlatform = _menu_Dev_Game.g_GamePlatform;
            foreach (int platform in existedPlatform)
            {
                if (platform == platformID)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsArcadeCabinetGame()
        {
            string myName = _menu_Dev_Game.uiObjects[146].transform.Find("Label").gameObject.GetComponent<Text>().text;
            if(myName == "Arcade Cabinet")
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}

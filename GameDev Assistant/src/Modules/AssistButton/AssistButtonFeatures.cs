using HarmonyLib;
using UnityEngine;
using GameDevAssistant.Config;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

namespace GameDevAssistant.Modules.AssistButton
{
    public partial class AssistButtonFeatures: MonoBehaviour
    {
        private GameObject _obj_Dev_Game;
        private Menu_DevGame _menu_Dev_Game;
        private genres _genres;
        private themes _themes;
        private GUI_Main _guiMain;
        private const string BUTTON_INPUTFIELD_NAME = "InputFieldName";
        public enum PlatformSlots { Four = 4 }

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
            FindScripts();
            _isInitPlatform = false;
        }

        /// <summary>
        /// AssistButtonの自動化の処理を設定します。
        /// AssistButtonを押下した際に実行されます。
        /// Set the automation process of the AssistButton.
        /// It will be executed when the AssistButton is pressed.
        /// </summary>
        public void SetAssistAutomationFeatures()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }
            Init();

            //ランダム名前設定自動化 | Random Name Setting Automation
            if (ConfigManager.IsAssistRandomNameEnabled.Value)
            {
                bool isInteractable = AssistButtonHelper.IsInteractableUIObjectByName<InputField>(_menu_Dev_Game.uiObjects, BUTTON_INPUTFIELD_NAME);
                if (isInteractable)
                {
                    _menu_Dev_Game.BUTTON_RandomGameName();
                }
            }

            //ジャンル自動化 | Genre Automation
            SetFitMainGenreAtRandom();
            SetFitSubGenreAtRandom();

            //テーマ自動化 | Theme Automation
            SetFitMainThemeAtRandom();
            SetFitSubThemeAtRandom();

            //ターゲット年齢層自動化 | Target Age GroupAutomation
            SetFitAgeTargetGroupAtRandom();

            //デザイン設定自動化　| Design Setting Automation
            if (ConfigManager.IsAssistAutoDesignSliderEnabled.Value)
            {
                _menu_Dev_Game.BUTTON_AutoDesignSettings();
            }

            //プラットフォーム自動化　| Platform Automation
            if (ConfigManager.IsAssistPlatformEnabled.Value)
            {
                for (int i = 0; i < (int)PlatformSlots.Four; i++)
                {
                    InitializePlatformSelection(i, false);
                }
            }

            //ライセンス自動化　| License Automation
            if (ConfigManager.IsAssistLicenseEnabled.Value)
            {
                InitializeLicenceSelection(false);
            }

            //ライセンス名設定有無 | License Name Setting
            SetLicenceName();

            //エンジン機能自動化 | Engine Function Automation
            if (ConfigManager.IsAssistEngineFeaturesEnabled.Value)
            {
                _menu_Dev_Game.BUTTON_AutoEngineFeature();
            }
            
            //全言語対応自動化 | All Language Support Automation
            if (ConfigManager.IsAssistAllLanguageEnabled.Value)
            {
                SetAllLanguage();
            }
        }

        //forschungSonstiges - Research Miscellaneous
    }
}

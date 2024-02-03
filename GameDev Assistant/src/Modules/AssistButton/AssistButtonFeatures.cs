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

        public void SetFeatures()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }
            Init();

            //名前自動化
            if (ConfigManager.IsAssistRandomNameEnabled.Value)
            {
                _menu_Dev_Game.BUTTON_RandomGameName();
            }

            //ジャンル自動化
            SetFitMainGenreAtRandom();
            SetFitSubGenreAtRandom();

            //テーマ自動化
            SetFitMainThemeAtRandom();
            SetFitSubThemeAtRandom();

            //ターゲット年齢自動化
            SetFitAgeTargetGroupAtRandom();

            //デザイン設定自動化
            if (ConfigManager.IsAssistAutoDesignSliderEnabled.Value)
            {
                _menu_Dev_Game.BUTTON_AutoDesignSettings();
            }

            //プラットフォーム自動化
            for(int i = 0; i < (int)PlatformSlots.Four; i++)
            {
                InitializePlatformSelection(i, false);
            }
        }

        //forschungSonstiges - Research Miscellaneous
    }
}

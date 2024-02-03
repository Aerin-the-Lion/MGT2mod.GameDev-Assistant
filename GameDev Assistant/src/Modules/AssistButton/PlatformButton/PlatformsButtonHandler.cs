using GameDevAssistant.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using GameDevAssistant.Config;
using GameDevAssistant.Modules.AssistButton;

namespace GameDevAssistant.Modules
{
    public class PlatformsButtonHandler : ButtonHandlerBase
    {
        public override string ButtonName => "Button_GameDevAssistant_Platforms";
        public override string ButtonTooltip => "Just press this for automatic setup platforms!";
        protected override string MainGamePath => "CanvasInGameMenu";
        protected override string GameObjectPath => "Menu_Dev_Game";
        protected override string OrgButtonPath => "WindowMain/Seite4/Button_AutoDesingSettings";

        protected override string MyButtonPath => "Menu_Dev_Game/WindowMain/Seite2/";

        private static PlatformsButtonHandler instance;
        public static PlatformsButtonHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlatformsButtonHandler();
                }
                return instance;
            }
        }

        protected override void CustomizePlacedButton(GameObject button)
        {
            base.CustomizePlacedButton(button);
            myButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(600.0f, 470.0f);
        }

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();
            // AssistButton特有の処理をここに書く
            if (!ConfigManager.IsModEnabled.Value) { return; }
            AssistButtonFeatures features = AssistButtonFeatures.Instance;
            features.Init();

            //プラットフォーム自動化
            for (int i = 0; i < (int)AssistButtonFeatures.PlatformSlots.Four; i++)
            {
                features.InitializePlatformSelection(i, false);
            }
        }
    }
}

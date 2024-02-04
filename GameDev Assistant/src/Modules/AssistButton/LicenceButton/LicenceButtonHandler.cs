
using UnityEngine;
using GameDevAssistant.Config;
using GameDevAssistant.Modules.AssistButton;

namespace GameDevAssistant.Modules
{
    public class LicenceButtonHandler : ButtonHandlerBase
    {
        public override string ButtonName => "Button_GameDevAssistant_Licence";
        public override string ButtonTooltip => "Just press this for automatic setup Licence!";
        protected override string MainGamePath => "CanvasInGameMenu";
        protected override string GameObjectPath => "Menu_Dev_Game";
        protected override string OrgButtonPath => "WindowMain/Seite4/Button_AutoDesingSettings";

        protected override string MyButtonPath => "Menu_Dev_Game/WindowMain/Seite1/";
        protected override string PicturePath => "GameDevAssistant/AssistButton";
        protected override string PictureName => "iconLicenceButton.png";

        private static LicenceButtonHandler instance;
        public static LicenceButtonHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LicenceButtonHandler();
                }
                return instance;
            }
        }

        protected override void CustomizePlacedButton(GameObject button)
        {
            base.CustomizePlacedButton(button);
            myButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(55.0f, 140.0f);
        }

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();
            // AssistButton特有の処理をここに書く
            if (!ConfigManager.IsModEnabled.Value) { return; }
            AssistButtonFeatures features = AssistButtonFeatures.Instance;
            features.Init();

                features.InitializeLicenceSelection(false);
        }
    }
}

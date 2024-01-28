using GameDevAssistant.Config;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevAssistant.Modules.PlatformHistory
{
    public class SelectButtonHandler : ButtonHandlerBase
    {
        public override string ButtonName => "Button_SelectPlatformHistory";
        public override string ButtonTooltip => "";
        protected override string MainGamePath => "CanvasInGameMenu";
        protected override string GameObjectPath => "Menu_Dev_Game";
        protected override string OrgButtonPath => "WindowMain/Seite2/Button_EngineKaufen";

        protected override string MyButtonPath => "Menu_Dev_Game/WindowMain/Seite2/";

        private static SelectButtonHandler instance;
        public static SelectButtonHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SelectButtonHandler();
                }
                return instance;
            }
        }

        protected override void CustomizeCreatedButton(GameObject button)
        {
            base.CustomizeCreatedButton(button);
            SetButtonShowName(button);
        }

        protected override void CustomizePlacedButton(GameObject button)
        {
            base.CustomizePlacedButton(button);
            myButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(490.0f, 515.0f);
        }

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();
            // AssistButton特有の処理をここに書く
            SelectButtonFeatures features = SelectButtonFeatures.Instance;
            features.Init();
        }

        private void SetButtonShowName(GameObject button)
        {
            try
            {
                Transform transform = button.transform.Find("Text");
                UnityEngine.GameObject.Destroy(transform.GetComponent<setText>());
                transform.GetComponent<Text>().text = ButtonShowName;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
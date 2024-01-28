using GameDevAssistant.Config;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevAssistant.Modules
{
    /*
    public class AssistButtonHandler
    {
        private static GameObject _orgButton;
        private static GameObject _assistButton;
        private static GameObject _menu_Dev_Game;
        private static string _buttonName = "Button_GameDevAssistant";
        public static string _buttonTooltip = "Assist your game development! Just press this for automatic setup!";

        public static string ButtonName { get => _buttonName; }
        public static string ButtonTooltip { get => _buttonTooltip; }

        private static GameObject CreateAssistButton()
        {
            _menu_Dev_Game = GameObject.Find("CanvasInGameMenu").transform.Find("Menu_Dev_Game").gameObject;
            _orgButton = _menu_Dev_Game.transform.Find("WindowMain/Seite4/Button_AutoDesingSettings").gameObject;
            return UnityEngine.Object.Instantiate(_orgButton);
        }

        /// <summary>
        /// GameDevメニューにアシストボタンを追加する
        /// in English: Add an assist button to the GameDev menu
        /// </summary>
        public static void AddAssistButtonToGameDevMenu()
        {
            //if (IsCharacterEditorButtonExists()) { return; }

            _assistButton = CreateAssistButton();
            CustomizeCreatedButton(_assistButton);
            SetButtonOnClickEvent(_assistButton);
        }

        // -----------------------------------------------------------------------

        private static void CustomizeCreatedButton(GameObject assistButton)
        {
            assistButton.name = _buttonName;
            //assistButton.GetComponent<tooltip>().c = "Assist your game developmen. it automates build!";
            CustomizePlacedButton(assistButton);
        }

        private static void SetButtonTooltip()
        {
            _assistButton.GetComponent<tooltip>().c = _buttonTooltip;
        }

        private static void CustomizePlacedButton(GameObject assistButton)
        {
            Transform mainMenu = _menu_Dev_Game.transform.Find("WindowMain/Seite1/");
            assistButton.transform.SetParent(mainMenu.transform);
            assistButton.transform.localScale = _orgButton.transform.localScale;
            assistButton.transform.localPosition = new Vector3(0f, 0f, 0.0f);
            RectTransform rt = assistButton.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(0.0f, 0.0f);
                rt.anchorMax = new Vector2(0.0f, 0.0f);
                rt.anchoredPosition = new Vector2(570.0f, 140.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void OnSelectCharacterEditorButtonClicked()
        {
            //ここにボタンのクリック時の処理を書く
            //in English: Write the processing when the button is clicked here
            AssistButtonFeatures.Init();

        }

        private static void SetButtonOnClickEvent(GameObject assistButton)
        {
            Button buttonComponent = assistButton.GetComponent<Button>();
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(OnSelectCharacterEditorButtonClicked);
        }
    }
    */

    public class AssistButtonHandler : ButtonHandlerBase
    {
        public override string ButtonName => "Button_GameDevAssistant";
        public override string ButtonTooltip => "Assist your game development! Just press this for automatic setup!";
        protected override string MainGamePath => "CanvasInGameMenu";
        protected override string GameObjectPath => "Menu_Dev_Game";
        protected override string OrgButtonPath => "WindowMain/Seite4/Button_AutoDesingSettings";

        protected override string MyButtonPath => "Menu_Dev_Game/WindowMain/Seite1/";

        private static AssistButtonHandler instance;
        public static AssistButtonHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssistButtonHandler();
                }
                return instance;
            }
        }

        protected override void CustomizePlacedButton(GameObject button)
        {
            base.CustomizePlacedButton(button);
            myButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(570.0f, 140.0f);
        }

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();
            // AssistButton特有の処理をここに書く
            AssistButtonFeatures features = AssistButtonFeatures.Instance;
            features.Init();
        }
    }
}

using GameDevAssistant.Config;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevAssistant.Modules
{
    public class AssistButtonHandler
    {
        public static GameObject orgButton;
        public static GameObject assistButton;
        public static GameObject menu_Dev_Game;
        public static string buttonName = "Button_GameDevAssistant";
        public static string buttonTooltip = "Assist your game development! Just press this for automatic setup!";

        private static GameObject CreateAssistButton()
        {
            menu_Dev_Game = GameObject.Find("CanvasInGameMenu").transform.Find("Menu_Dev_Game").gameObject;
            orgButton = menu_Dev_Game.transform.Find("WindowMain/Seite4/Button_AutoDesingSettings").gameObject;
            return UnityEngine.Object.Instantiate(orgButton);
        }

        /// <summary>
        /// 従業員メニューの個人メニューに、Character Editorを開くボタンを追加する関数
        /// in English: Add a button to open Character Editor to the personal menu of the employee menu
        /// </summary>
        public static void AddAssistButtonToGameDevMenu()
        {
            //if (IsCharacterEditorButtonExists()) { return; }

            assistButton = CreateAssistButton();
            CustomizeCreatedButton(assistButton);
            SetButtonOnClickEvent(assistButton);
        }

        private static void CustomizeCreatedButton(GameObject assistButton)
        {
            assistButton.name = buttonName;
            //assistButton.GetComponent<tooltip>().c = "Assist your game developmen. it automates build!";
            CustomizePlacedButton(assistButton);
        }

        public static void SetButtonTooltip()
        {
            assistButton.GetComponent<tooltip>().c = buttonTooltip;
        }

        private static void CustomizePlacedButton(GameObject assistButton)
        {
            Transform mainMenu = menu_Dev_Game.transform.Find("WindowMain/Seite1/");
            assistButton.transform.SetParent(mainMenu.transform);
            assistButton.transform.localScale = orgButton.transform.localScale;
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
            AssistFeatures.Init();

        }

        private static void SetButtonOnClickEvent(GameObject assistButton)
        {
            Button buttonComponent = assistButton.GetComponent<Button>();
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(OnSelectCharacterEditorButtonClicked);
        }
    }
}

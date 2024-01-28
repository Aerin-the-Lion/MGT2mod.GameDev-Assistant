using System.Data;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevAssistant.Modules
{
    public class ButtonHandlerBase
    {
        protected GameObject orgButton;
        protected GameObject myButton;
        protected GameObject orgInstance;
        protected Transform myTransform;
        /// <summary>
        /// Name of the button.
        /// </summary>
        public virtual string ButtonName => "Button_Base";
        public virtual string ButtonShowName => "Base Button Name";
        public virtual string ButtonTooltip => "Base Tooltip";
        protected virtual string MainGamePath => "CanvasInGameMenu";
        protected virtual string GameObjectPath => "CanvasInGameMenu/Menu_Dev_Game";
        protected virtual string OrgButtonPath => "WindowMain/Seite4/Button_AutoDesingSettings";

        protected virtual string MyButtonPath => "CanvasInGameMenu/Menu_Dev_Game/WindowMain/Seite1/";

        /// <summary>
        /// 派生クラスによってクローン化されたボタンをゲーム内に追加します。
        /// Instantiate the button cloned by the derived class into the game.
        /// </summary>
        public virtual void AddButton()
        {
            myButton = CreateButton();
            CustomizeCreatedButton(myButton);
            SetButtonOnClickEvent(myButton);
        }

        protected virtual GameObject CreateButton()
        {
            orgInstance = GameObject.Find(MainGamePath).transform.Find(GameObjectPath).gameObject;
            orgButton = orgInstance.transform.Find(OrgButtonPath).gameObject;
            return UnityEngine.Object.Instantiate(orgButton);
        }

        protected virtual Transform FindMyTransformPath()
        {
            myTransform = GameObject.Find(MainGamePath).transform.Find(MyButtonPath);
            return myTransform;
        }

        protected virtual void CustomizeCreatedButton(GameObject button)
        {
            button.name = ButtonName;
            CustomizePlacedButton(button);
        }

        protected virtual void SetButtonTooltip()
        {
            myButton.GetComponent<tooltip>().c = ButtonTooltip;
        }

        protected virtual void CustomizePlacedButton(GameObject button)
        {
            Transform myPath = FindMyTransformPath();
            button.transform.SetParent(myPath.transform);
            button.transform.localScale = orgButton.transform.localScale;
            button.transform.localPosition = new Vector3(0f, 0f, 0.0f);
            RectTransform rt = button.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(0.0f, 0.0f);
                rt.anchorMax = new Vector2(0.0f, 0.0f);
                rt.anchoredPosition = new Vector2(0.0f, 0.0f);
            }
        }

        protected virtual void OnButtonClicked()
        {
            // ボタンがクリックされたときの基本的な処理
        }

        protected virtual void SetButtonOnClickEvent(GameObject button)
        {
            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(OnButtonClicked);
        }

        /*
        protected virtual void SetButtonShowName(GameObject button)
        {
            try
            {
                button.GetComponentInChildren<Text>().text = ButtonShowName;
            }catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }

        */
    }
}

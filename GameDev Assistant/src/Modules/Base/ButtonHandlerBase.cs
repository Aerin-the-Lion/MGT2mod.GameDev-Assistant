using BepInEx;
using System.Data;
using System.IO;
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
        protected virtual string PicturePath => "GameDevAssistant/AssistButton";
        protected virtual string PictureName => null;

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
            CustomizeImage(button);
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

        /// <summary>
        /// BepInExのプラグインフォルダから画像を読み込んでボタンの画像を変更します。
        /// Load an image from the BepInEx plugin folder and change the button image.
        /// </summary>
        /// <param name="button"></param>
        protected virtual void CustomizeImage(GameObject button)
        {
            if(PictureName == null) { return; }
            // 画像を変更する処理
            string imagePath = Path.Combine(Paths.PluginPath, PicturePath, PictureName);
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2); // サイズは適当でOK、LoadImageが適切にリサイズする
            if (texture.LoadImage(imageBytes))
            {
                // 新しい非圧縮テクスチャを作成
                Texture2D uncompressedTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
                uncompressedTexture.SetPixels(texture.GetPixels());
                uncompressedTexture.filterMode = FilterMode.Point;
                uncompressedTexture.Apply();
                myButton.transform.GetChild(0).GetComponent<Image>().sprite = 
                    Sprite.Create(uncompressedTexture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
            else
            {
                Debug.LogError("画像のロードに失敗しました: " + PictureName);
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

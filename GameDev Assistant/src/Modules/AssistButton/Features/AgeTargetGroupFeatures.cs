using GameDevAssistant.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace GameDevAssistant.Modules.AssistButton
{
    public partial class AssistButtonFeatures
    {
        public const string BUTTON_AGETARGETGROUP = "Button_Zielgruppe";
        private void SetFitAgeTargetGroupAtRandom()
        {
            if (!ConfigManager.IsModEnabled.Value || !ConfigManager.IsAssistAgeTargetEnabled.Value) return;
            //check if the button is interactable
            bool isButtonInteractable = AssistButtonHelper.IsInteractableUIObjectByName<Button>(_menu_Dev_Game.uiObjects, BUTTON_AGETARGETGROUP);
            if (!isButtonInteractable) { return; }

            // ---------------------------------------------------------------------------------------

            Menu_DevGame_Zielgruppe menuAge = _guiMain.uiObjects[60].GetComponent<Menu_DevGame_Zielgruppe>();


            List<int> validAgeIndices = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                if (_genres.IsTargetGroup(_menu_Dev_Game.g_GameMainGenre, i))
                {
                    validAgeIndices.Add(i);
                }
            }

            if (validAgeIndices.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validAgeIndices.Count);
                int selectedAgeIndex = validAgeIndices[randomIndex];
                _menu_Dev_Game.SetZielgruppe(selectedAgeIndex); // ここでランダムな数字を設定
            }
            else
            {
                // 対象となる年齢グループが見つからなかった場合の処理
            }
        }
    }
}

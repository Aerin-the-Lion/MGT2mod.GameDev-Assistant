using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant.Modules.AssistButton
{
    public partial class AssistButtonFeatures
    {
        private void SetAllLanguage()
        {
            for (int i = 0; i < _menu_Dev_Game.g_GameLanguage.Length; i++)
            {
                if (_menu_Dev_Game.g_GameLanguage[i]) { continue; }
                _menu_Dev_Game.SetLanguage(i);
            }
        }
    }
}

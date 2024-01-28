using GameDevAssistant.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant.Modules.PlatformHistory
{
    public class SelectButtonManager
    {
        public static void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }

            SelectButtonHandler button = SelectButtonHandler.Instance;
            button.AddButton();
        }
    }
}

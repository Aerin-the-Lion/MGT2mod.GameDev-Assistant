using GameDevAssistant.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant.Modules.AssistButton
{
    public class AssistButtonManager
    {
        public static void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }

            AssistButtonHandler assistbutton = AssistButtonHandler.Instance;
            assistbutton.AddButton();
        }
    }
}

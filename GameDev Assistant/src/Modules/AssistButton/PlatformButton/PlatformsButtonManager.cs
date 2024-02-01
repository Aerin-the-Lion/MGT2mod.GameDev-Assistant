using GameDevAssistant.Config;
using GameDevAssistant.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant.Modules
{
    public class PlatformsButtonManager
    {
        public static void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }

            PlatformsButtonHandler platformsbutton = PlatformsButtonHandler.Instance;
            platformsbutton.AddButton();
        }
    }
}

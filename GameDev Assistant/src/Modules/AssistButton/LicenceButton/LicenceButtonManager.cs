using GameDevAssistant.Config;
using GameDevAssistant.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameDevAssistant.Modules
{
    public class LicenceButtonManager
    {
        public static void Init()
        {
            if (!ConfigManager.IsModEnabled.Value) { return; }

            LicenceButtonHandler Licencebutton = LicenceButtonHandler.Instance;
            Licencebutton.AddButton();
        }
    }
}

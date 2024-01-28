using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant.Modules.PlatformHistory
{
    public class SelectButtonFeatures
    {
        private static SelectButtonFeatures _instance;
        public static SelectButtonFeatures Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SelectButtonFeatures();
                }
                return _instance;
            }
        }

        public void Init()
        {

        }
    }
}

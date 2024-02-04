using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameDevAssistant
{
    public class Helper
    {
        public static Color GetColor(string colorName)
        {
            Color color = Color.white;
            switch (colorName)
            {
                case "Black":
                    color = Color.black;
                    break;
                case "Blue":
                    color = Color.blue;
                    break;
                case "Brown":
                    color = new Color(0.6f, 0.3f, 0.0f);
                    break;
                case "Cyan":
                    color = Color.cyan;
                    break;
                case "DarkBlue":
                    color = new Color(0.0f, 0.0f, 0.5f);
                    break;
                case "Green":
                    color = Color.green;
                    break;
                case "Grey":
                    color = Color.grey;
                    break;
                case "LightBlue":
                    color = new Color(0.6f, 0.6f, 1.0f);
                    break;
                case "Magenta":
                    color = Color.magenta;
                    break;
                case "Maroon":
                    color = new Color(0.5f, 0.0f, 0.0f);
                    break;
                case "Navy":
                    color = new Color(0.0f, 0.0f, 0.5f);
                    break;
                case "Olive":
                    color = new Color(0.5f, 0.5f, 0.0f);
                    break;
                case "Orange":
                    color = new Color(1.0f, 0.5f, 0.0f);
                    break;
                case "Purple":
                    color = new Color(0.5f, 0.0f, 0.5f);
                    break;
                case "Red":
                    color = Color.red;
                    break;
                case "Silver":
                    color = new Color(0.75f, 0.75f, 0.75f);
                    break;
                case "Teal":
                    color = new Color(0.0f, 0.5f, 0.5f);
                    break;
                case "White":
                    color = Color.white;
                    break;
                case "Yellow":
                    color = Color.yellow;
                    break;
            }
            return color;
        }
        public static int GetPlatformFilter(string filterName)
        {
            int filter = 0;
            switch (filterName)
            {
                case "Name":
                    filter = 0;
                    break;
                case "Manufacturer":
                    filter = 1;
                    break;
                case "ReleaseDate":
                    filter = 2;
                    break;
                case "TechnologyLevel":
                    filter = 3;
                    break;
                case "PurchasePrice":
                    filter = 4;
                    break;
                case "MarketShare":
                    filter = 5;
                    break;
                case "AvailableGames":
                    filter = 6;
                    break;
                case "DevelopmentCosts":
                    filter = 7;
                    break;
                case "PlatformType":
                    filter = 8;
                    break;
                case "ActiveUsers":
                    filter = 9;
                    break;
            }
            return filter;
        }
    }
}

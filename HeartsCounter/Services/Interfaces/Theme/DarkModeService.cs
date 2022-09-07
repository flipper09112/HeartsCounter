using HeartsCounter.Services.Implementations.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Services.Interfaces.Theme
{
    public class DarkModeService : IDarkModeService
    {
        public static string ThemeAutoKey = "ThemeAutoKey";
        public static string DarkModeKey = "DarkModeKey";

        public bool GetIsThemeAuto()
        {
            return Preferences.Get(ThemeAutoKey, true);
        }

        public void SetDarkMode(bool darkMode)
        {
            Preferences.Set(DarkModeKey, darkMode);
        }

        public void SetThemeAuto(bool themeAuto)
        {
            Preferences.Set(ThemeAutoKey, themeAuto);
        }
    }
}

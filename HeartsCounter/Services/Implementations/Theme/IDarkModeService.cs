using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Services.Implementations.Theme
{
    public interface IDarkModeService
    {
        bool GetIsThemeAuto();
        void SetDarkMode(bool darkMode);
        void SetThemeAuto(bool themeAuto);
    }
}

using HeartsCounter.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.TemplateSelectors
{
    public class SettingsItemSelector : DataTemplateSelector
    {
        public DataTemplate SettingTitleTemplate { get; set; }
        public DataTemplate SettingItemTemplate { get; set; }
        public DataTemplate SettingItemDarkModeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is SettingsItemTitle settingsItemTitle)
                return SettingTitleTemplate;

            if (item is SettingsItemElement settingsItemElement)
                return SettingItemTemplate;

            if (item is SettingsItemElementTheme settingsItemElementTheme)
                return SettingItemDarkModeTemplate;
            
            return null;
        }
    }
}

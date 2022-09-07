using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Pages.Settings;
using HeartsCounter.Services.Implementations.Theme;
using HeartsCounter.Services.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.Settings
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private IDatabaseManagerService _databaseManagerService;
        private IDarkModeService _darkModeService;

        [ObservableProperty]
        private List<SettingsItem> _settingsItemsList;

        public SettingsViewModel(IDatabaseManagerService databaseManagerService,
                                 IDarkModeService darkModeService)
        {
            _databaseManagerService = databaseManagerService;
            _darkModeService = darkModeService;

            SettingsItemsList = GetSettingsItems();
        }

        private List<SettingsItem> GetSettingsItems()
        {
            var list = new List<SettingsItem>();

            list.Add(new SettingsItemTitle()
            {
                Name = "App Theme"
            });
            list.Add(new SettingsItemElementTheme()
            {
                Command = SetThemeAutoCommand,
                SetDarkModeCommand = SetDarkModeCommand,
                AutoImage = "ic_appearance",
                Auto = _darkModeService.GetIsThemeAuto(),
                DarkMode = Application.Current.UserAppTheme == AppTheme.Dark,
                AutoName = "Appearance Auto",
                DarkImage = "ic_bulb",
                DarkName = "Dark mode"
            });

            list.Add(new SettingsItemTitle()
            {
                Name = "Data"
            });
            list.Add(new SettingsItemElement()
            {
                Name = "Clear all data",
                Image = "ic_clear_data",
                Command = ClearAllDataCommand
            });

            list.Add(new SettingsItemTitle()
            {
                Name = "Info"
            });
            list.Add(new SettingsItemElement()
            {
                Name = "App information",
                Image = "ic_info",
                OpenNewPage = true,
                Command = ShowAppInfoCommand
            });

            return list;
        }

        [RelayCommand]
        public async void CloseApp()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Do you want close app?", string.Empty, "Yes", "No");
            if (answer) 
                Application.Current.Quit();
        }

        [RelayCommand]
        public async void ClearAllData()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Are you sure that want clear all games?", "If you accept you will lose all games data", "Yes", "No");
            if(answer)
                _databaseManagerService.ClearAllData();
        }

        [RelayCommand]
        public void SetThemeAuto(bool themeAuto)
        {
            _darkModeService.SetThemeAuto(themeAuto);
        }

        [RelayCommand]
        public void SetDarkMode(bool darkMode)
        {
            _darkModeService.SetDarkMode(darkMode);
            Application.Current.UserAppTheme = darkMode ? AppTheme.Dark : AppTheme.Light;
        }

        [RelayCommand]
        public void ShowAppInfo()
        {
            IsBusy = true;
            Shell.Current.GoToAsync(nameof(AppInfoPage));
            IsBusy = false;
        }
    }

    public partial class SettingsItem : ObservableObject
    {
        public string Name { get; set; } 
        public string Image { get; set; }

        public IRelayCommand Command { get; set; }
    }

    public class SettingsItemTitle : SettingsItem
    {

    }
    public class SettingsItemElement : SettingsItem
    {
        public bool OpenNewPage { get; set; }
    }

    public partial class SettingsItemElementTheme : SettingsItem
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Opacity))]
        [NotifyPropertyChangedFor(nameof(ShowDarkTogle))]
        [NotifyPropertyChangedFor(nameof(ThumColor))]
        private bool _auto;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DarkThumColor))]
        private bool _darkMode; 

        public string AutoImage { get; set; }
        public string AutoName { get; set; }
        public string DarkImage { get; set; }
        public string DarkName { get; set; }
        public Color ThumColor => Auto ? Colors.Green : Colors.Gray;
        public Color DarkThumColor => DarkMode ? Colors.Green : Colors.Gray;

        public bool ShowDarkTogle => !Auto;
        public float Opacity => Auto ? 0.3f : 1;

        public IRelayCommand<bool> SetDarkModeCommand { get; set; }

        partial void OnAutoChanged(bool value)
        {
            Command.Execute(value);
        }

        partial void OnDarkModeChanged(bool value)
        {
            SetDarkModeCommand.Execute(value);
        }
    }
    
}

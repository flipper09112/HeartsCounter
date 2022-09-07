using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.Settings
{
    public partial class AppInfoViewModel : BaseViewModel
    {


        [RelayCommand]
        public async void OpenLinkedin()
        {
            try
            {
                Uri uri = new Uri("https://www.linkedin.com/in/filipe-torres-383819160/");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        [RelayCommand]
        public async void OpenTwitter()
        {
            try
            {
                Uri uri = new Uri("https://twitter.com/flipper09112");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        [RelayCommand]
        public async void OpenGithub()
        {
            try
            {
                Uri uri = new Uri("https://github.com/flipper09112");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }
    }
}

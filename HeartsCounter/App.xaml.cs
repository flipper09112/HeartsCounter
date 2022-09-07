using HeartsCounter.Services.Interfaces.Theme;

namespace HeartsCounter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		UserAppTheme = Preferences.Get(DarkModeService.ThemeAutoKey, true) ? PlatformAppTheme :
                       Preferences.Get(DarkModeService.DarkModeKey, false) ? AppTheme.Dark : AppTheme.Light;
    }
}

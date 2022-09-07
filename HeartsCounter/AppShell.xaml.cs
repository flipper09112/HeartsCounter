using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Pages.History;
using HeartsCounter.Pages.New_Game;
using HeartsCounter.Pages.Settings;
using HeartsCounter.Views.BottomSheetsViews;

namespace HeartsCounter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NewGamePage), typeof(NewGamePage));
		Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
		Routing.RegisterRoute(nameof(FinishedGamePage), typeof(FinishedGamePage));
		Routing.RegisterRoute(nameof(GamesHistoryPage), typeof(GamesHistoryPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		Routing.RegisterRoute(nameof(AppInfoPage), typeof(AppInfoPage));
    }
}

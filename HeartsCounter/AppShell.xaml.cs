using HeartsCounter.Pages.New_Game;

namespace HeartsCounter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NewGamePage), typeof(NewGamePage));
	}
}

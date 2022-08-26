using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Pages.New_Game;
using HeartsCounter.Views.BottomSheetsViews;

namespace HeartsCounter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NewGamePage), typeof(NewGamePage));
		Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
		Routing.RegisterRoute(nameof(AddRoundBottomSheetView), typeof(AddRoundBottomSheetView));
    }
}

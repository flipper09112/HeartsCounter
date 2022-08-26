using HeartsCounter.ViewModels.CurrentGame;

namespace HeartsCounter.Pages.CurrentGame;

public partial class GamePage : ContentPage
{
	public GamePage(GameViewModel gameViewModel)
	{
		InitializeComponent();

		BindingContext = gameViewModel;
    }

	private void AddRoundButtonClick(object sender, EventArgs e)
	{
        ((GameViewModel)BindingContext).ShowBottomSheet(this, true);
    }
}
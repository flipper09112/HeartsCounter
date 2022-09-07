using HeartsCounter.ViewModels.CurrentGame;

namespace HeartsCounter.Pages.CurrentGame;

public partial class FinishedGamePage : ContentPage
{
	public FinishedGamePage(FinishedGameViewModel finishedGameViewModel)
	{
		InitializeComponent();

		BindingContext = finishedGameViewModel;
    }

	protected override bool OnBackButtonPressed()
	{
		Shell.Current.GoToAsync("../..");
		return true;
	}
}
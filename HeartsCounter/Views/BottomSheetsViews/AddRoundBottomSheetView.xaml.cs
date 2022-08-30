using HeartsCounter.ViewModels.CurrentGame;

namespace HeartsCounter.Views.BottomSheetsViews;

public partial class AddRoundBottomSheetView : ContentPage
{
    private GameViewModel _gameViewModel;

    public AddRoundBottomSheetView(GameViewModel gameViewModel)
	{
        _gameViewModel = gameViewModel;

        InitializeComponent();
		BackgroundColor = new Color(0, 0, 0, 0);
		BindingContext = gameViewModel;
	}
}
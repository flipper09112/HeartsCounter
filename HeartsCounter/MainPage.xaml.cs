using HeartsCounter.ViewModels;

namespace HeartsCounter;

public partial class MainPage : ContentPage
{
	private MainViewModel _mainViewModel;

	public MainPage(MainViewModel vm)
	{
		_mainViewModel = vm;

        InitializeComponent();
		BindingContext = vm;
	}

	private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
        Frame frame = (Frame)sender;

        var animation = new Animation(v => frame.BackgroundColor = Colors.Gray);
		animation.Commit(this, "animation");
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_mainViewModel.GetOptions();

		MainCollectionView.ItemsSource = null;
		MainCollectionView.ItemsSource = _mainViewModel.Options;
	}
}


using HeartsCounter.ViewModels.Settings;

namespace HeartsCounter.Pages.Settings;

public partial class AppInfoPage : ContentPage
{
	public AppInfoPage(AppInfoViewModel appInfoViewModel)
	{
		InitializeComponent();

		BindingContext = appInfoViewModel;

    }
}
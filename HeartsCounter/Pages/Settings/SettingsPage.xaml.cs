using HeartsCounter.ViewModels.Settings;

namespace HeartsCounter.Pages.Settings;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();

		BindingContext = settingsViewModel;
    }
}
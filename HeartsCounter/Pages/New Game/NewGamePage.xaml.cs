using HeartsCounter.ViewModels.NewGame;

namespace HeartsCounter.Pages.New_Game;

public partial class NewGamePage : ContentPage
{
	private NewGameViewModel _newGameViewModel;

	private List<Label> _sliderNumbers;

	public NewGamePage(NewGameViewModel newGameViewModel)
	{
		_newGameViewModel = newGameViewModel;

        InitializeComponent();
		BindingContext = newGameViewModel;

		_sliderNumbers = new List<Label>()
		{
			SliderOne,
			SliderTwo,
			SliderThree,
			SliderFour,
			SliderFive,
			SliderSix,
			SliderSeven,
			SliderHeight
		};

		HideNumberLabels(1);
    }

	private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
	{
        int value = (int)e.NewValue;

		Slider.Value = value;
		_newGameViewModel.NumberOfPlayers = value;

		HideNumberLabels(value);
    }

	private void HideNumberLabels(int value)
	{
		foreach(var label in _sliderNumbers)
		{
			if (label == SliderOne || label == SliderHeight)
            {
                label.IsVisible = true;
				continue;
            }

			label.IsVisible = (_sliderNumbers.IndexOf(label) + 1) == value;
		}
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
		int value = _newGameViewModel.SpadesQueenPointsValues[selectedIndex];

		_newGameViewModel.SelectSpadesQueenPointsCommand.Execute(value);
	}

	private void EditorTextChanged(object sender, TextChangedEventArgs e)
    { 
		//lets the Entry be empty
        if (string.IsNullOrEmpty(e.NewTextValue)) return;

        if (!int.TryParse(e.NewTextValue, out int value))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
}
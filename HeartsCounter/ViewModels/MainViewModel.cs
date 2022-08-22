using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Pages.New_Game;
using System.Windows.Input;
namespace HeartsCounter.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    public List<Option> _options;

    [ObservableProperty]
    public string _version = "1.1.0";

    public MainViewModel()
    {
        _options = new List<Option>()
        {
            new Option()
            {
                OptionType = OptionTypeEnum.NewGame,
                Name = "New Game",
                Image = "ic_new_game.svg"
            },
            new Option()
            {
                OptionType = OptionTypeEnum.History,
                Name = "History",
                Image = "ic_history.svg"
            },
            new Option()
            {
                OptionType = OptionTypeEnum.Options,
                Name = "Options",
                Image = "ic_settings.svg"
            }
        };
    }

    [RelayCommand]
    public void OptionClick(Option option)
    {
        switch(option.OptionType)
        {
            case OptionTypeEnum.NewGame:
                ShowNewGamePage();
                break;
            case OptionTypeEnum.History:
                break;
            case OptionTypeEnum.Options:
                break;
            case OptionTypeEnum.None:
            default:
                break;
        }
    }

    private async void ShowNewGamePage()
    {
        await Shell.Current.GoToAsync(nameof(NewGamePage), true);
    }
}

public class Option
{
    public OptionTypeEnum OptionType { get; set; } = OptionTypeEnum.None;
    public string Name { get; set; }
    public string Image { get; set; }
}

public enum OptionTypeEnum
{
    None = 0,
    NewGame = 1,
    Options = 2,
    History = 3,
}
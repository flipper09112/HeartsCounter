using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Pages.History;
using HeartsCounter.Pages.New_Game;
using HeartsCounter.Pages.Settings;
using HeartsCounter.Services.Interfaces.Games;

namespace HeartsCounter.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private IGameDbManagerService _gameDbManagerService;

    [ObservableProperty]
    public List<Option> _options;

    [ObservableProperty]
    public string _version = "1.1.0";

    public MainViewModel(IGameDbManagerService gameDbManagerService)
    {
        _gameDbManagerService = gameDbManagerService;

        GetOptions();
    }

    public void GetOptions()
    {
        _options = new List<Option>();

        if(_gameDbManagerService?.GetCurrentGame() != null)
        {
            _options.Add(new Option()
            {
                OptionType = OptionTypeEnum.CurrentGame,
                Name = "Current Game",
                Image = "ic_game_on"
            });
        }

        _options.Add(new Option()
        {
            OptionType = OptionTypeEnum.NewGame,
            Name = "New Game",
            Image = "ic_new_game"
        });

        _options.Add(new Option()
        {
            OptionType = OptionTypeEnum.History,
            Name = "History",
            Image = "ic_history"
        });

        _options.Add(new Option()
        {
            OptionType = OptionTypeEnum.Options,
            Name = "Options",
            Image = "ic_settings"
        });
    }

    [RelayCommand]
    public void OptionClick(Option option)
    {
        switch(option.OptionType)
        {
            case OptionTypeEnum.CurrentGame:
                ShowCurrentGameAsync();
                break;
            case OptionTypeEnum.NewGame:
                ShowNewGamePage();
                break;
            case OptionTypeEnum.History:
                ShowHistoryPage();
                break;
            case OptionTypeEnum.Options:
                ShowSettingsPage();
                break;
            case OptionTypeEnum.None:
            default:
                break;
        }
    }

    private async void ShowSettingsPage()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync(nameof(SettingsPage), true);
        IsBusy = false;
    }

    private async void ShowHistoryPage()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync(nameof(GamesHistoryPage), true);
        IsBusy = false;
    }

    private async void ShowCurrentGameAsync()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync(nameof(GamePage), true);
        IsBusy = false;
    }

    private async void ShowNewGamePage()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync(nameof(NewGamePage), true);
        IsBusy = false;
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
    CurrentGame = 4,
}
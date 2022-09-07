using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Models;
using HeartsCounter.Models.Games;
using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Pages.New_Game;
using HeartsCounter.Services.Implementations.Games;
using HeartsCounter.Services.Interfaces.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.NewGame
{
    public partial class NewGameViewModel : BaseViewModel
    {
        private readonly IGameDbManagerService _gameDbManagerService;

        //viewmodel properties
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _newGameName;

        [ObservableProperty]
        private string _createGameButtonText;

        [ObservableProperty]
        private string _spadesQueenPointsLabel;

        [ObservableProperty]
        private string _chestnutBurstLabel;

        [ObservableProperty]
        private List<int> _spadesQueenPointsValues;

        [ObservableProperty]
        private bool _canCreateGame;

        public int MinPlayers { get; set; } = 1;
        public int MaxPlayers { get; set; } = 8;

        //form properties
        [ObservableProperty]
        private int _numberOfPlayers = 1;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateGameCommand))]
        private List<Player> _players;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateGameCommand))]
        private int _spadesQueenPoints = -1;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateGameCommand))]
        private int? _chestnutPoints;

        public NewGameViewModel(IGameDbManagerService gameDbManagerService)
        {
            _gameDbManagerService = gameDbManagerService;

            Title = "New Game";
            NewGameName = "Copas";
            CreateGameButtonText = "Create game";
            SpadesQueenPointsLabel = "Spades Queen Points";
            ChestnutBurstLabel = "Chestnut Burst";

            Players = new List<Player>()
            {
                new Player()
                {
                    PlayerNumber = 1,
                }
            };

            SpadesQueenPointsValues = new List<int>() { 10, 11, 12, 13, 14, 15 };  
        }

        
        [RelayCommand]
        public void SelectSpadesQueenPoints(int value)
        {
            SpadesQueenPoints = value;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteCreateGame))]
        public async Task CreateGameAsync()
        {
            _gameDbManagerService.AddNewGame(new Game()
            {
                GameName = NewGameName,
                GameType = GameTypeEnum.Copas,
                PlayerList = Players,
                AscendentPontuation = true,
                SpadesQueenPointsValue = SpadesQueenPoints,
                EndScoreValue = ChestnutPoints.Value,
                StartDate = DateTime.Now
            });

            await Shell.Current.GoToAsync("../" + nameof(GamePage), true);
        }

        public bool CanExecuteCreateGame()
        {
            CanCreateGame = _players.All(player => !string.IsNullOrEmpty(player.Name)) && _spadesQueenPoints != -1 && _chestnutPoints != null;
            return CanCreateGame;
        }

        partial void OnNumberOfPlayersChanged(int value)
        {
            var _tempList = CopyPlayersList();

            if(Players.Count < value)
            {
                for(int i = 0; i < value - Players.Count; i++)
                {
                    _tempList.Add(new Player() { PlayerNumber = Players.Last().PlayerNumber + 1 });
                }
            }
            else if(Players.Count > value)
            {
                _tempList.RemoveRange(value, Players.Count - value);
            }

            CreateGameCommand.NotifyCanExecuteChanged();
            Players = _tempList;
        }

        private List<Player> CopyPlayersList()
        {
            List<Player> newList = new List<Player>(Players.Count);

            Players.ForEach((item) =>
            {
                newList.Add((Player)item.Clone());
            });

            return newList;
        }
    }
}

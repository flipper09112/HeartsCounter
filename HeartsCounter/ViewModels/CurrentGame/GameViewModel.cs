using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Models;
using HeartsCounter.Models.Games;
using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Services.Implementations.Games;
using HeartsCounter.Services.Interfaces.CrossPlatform;
using HeartsCounter.Services.Interfaces.Database;
using HeartsCounter.Services.Interfaces.Games;
using HeartsCounter.Views.BottomSheetsViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.CurrentGame
{
    public partial class GameViewModel : BaseViewModel
    {
        private IGameDbManagerService _gameDbManagerService;
        private IBottomSheetDialogService _bottomSheetDialogService;

        [ObservableProperty]
        private List<Player> _players;

        [ObservableProperty]
        private List<NewRoundModel> _newRoundModelList;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _addRoundBtnText;

        [ObservableProperty]
        private Game _game;

        [ObservableProperty]
        private ObservableCollection<Round> _rounds;

        [ObservableProperty]
        private List<int> _heartsPointsPossibleValues;

        [ObservableProperty]
        private bool _fromHistoryPage;

        public GameViewModel(IGameDbManagerService gameDbManagerService,
                             IBottomSheetDialogService bottomSheetDialogService)
        {
            _gameDbManagerService = gameDbManagerService;
            _bottomSheetDialogService = bottomSheetDialogService;

            Game = _gameDbManagerService.SelectedHistoryGame ?? _gameDbManagerService.GetCurrentGame();
            _fromHistoryPage = _gameDbManagerService.SelectedHistoryGame == null;
            _gameDbManagerService.SelectedHistoryGame = null;

            Title = Game.GameName;
            AddRoundBtnText = "Add Round";
            Players = Game.PlayerList;

            GetRounds();

            HeartsPointsPossibleValues = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        }

        private void GetRounds()
        {
            var rounds = new ObservableCollection<Round>();

            rounds.Add(new RoundHeader()
            {
                RoundHeaderTitle = "Rounds",
                Players = Game.PlayerList
            });

            List<List<int>> roundPoints = new List<List<int>>();
            GetAllPointsPlayerHistory(roundPoints);

            for (int i = 0; i < roundPoints[0].Count; i++)
            {
                rounds.Add(new RoundData()
                {
                    RoundNumber = string.Format("#{0}", i),
                    RoundNumberValue = i
                });

                foreach(var points in roundPoints)
                {
                    ((RoundData)rounds[i + 1]).RoundPoints.Add(points[i]);
                }
            }

            Rounds = rounds;
        }

        private void GetAllPointsPlayerHistory(List<List<int>> roundPoints)
        {
            for (int i = 0; i < Game.PlayerList.Count; i++)
            {
                roundPoints.Add(Game.PlayerList[i].Points.ToList());
            }
        }

        internal void ShowBottomSheet(Page page, bool dimDismiss)
        {
            var newRoundModelList = new List<NewRoundModel>();
            Players.ForEach(player => newRoundModelList.Add(new NewRoundModel(player, ClearSpadesQueenSelection, AddRoundCommand)));
            NewRoundModelList = newRoundModelList;

            var view = new AddRoundBottomSheetView(this);
            _bottomSheetDialogService.ShowBottomSheet(page, view, dimDismiss);
        }

        private void ClearSpadesQueenSelection(int playerNumber)
        {
            _newRoundModelList?.ForEach(item =>
            {
                if(item.Player.PlayerNumber != playerNumber)
                {
                    item.WinSpadesQueen = false;
                }
            });
        }

        [ObservableProperty]
        private bool _canAddNewRound;

        [RelayCommand(CanExecute = nameof(CanAddRound))]
        public void AddRound()
        {
            List<int> points = new List<int>();
            foreach (var newRoundData in NewRoundModelList)
            {
                var player = newRoundData.Player;
                var playerPoints = GetNewPointsValue(player, newRoundData);
                player.Points.Add(playerPoints);
                points.Add(playerPoints);
            }

            var rounds = Rounds;
            var lastRound = Rounds.Last();
            Rounds.Remove(lastRound);


            rounds.Add(lastRound);
            rounds.Add(new RoundData()
            {
                RoundNumber = string.Format("#{0}", Rounds.Count - 1),
                RoundNumberValue = Rounds.Count - 1,
                RoundPoints = points
            });

            Rounds = null;
            Rounds = rounds;

            _gameDbManagerService.SaveGame(Game);
            _bottomSheetDialogService.HideCurrentBottomSheet();

            ValidateGameFinished();
        }

        private async void ValidateGameFinished()
        {
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;

            Game.PlayerList.ForEach(player =>
            {
                var points = player.Points.Last();

                if (points > maxValue)
                    maxValue = points;

                if (points < minValue)
                    minValue = points;
            });

            if(Game.AscendentPontuation && maxValue > Game.EndScoreValue)
            {
                Game.FinishDate = DateTime.Now;
                await Shell.Current.GoToAsync(nameof(FinishedGamePage), true);
            }
            else if(!Game.AscendentPontuation && minValue < Game.EndScoreValue)
            {
                Game.FinishDate = DateTime.Now;
                await Shell.Current.GoToAsync(nameof(FinishedGamePage), true);
            }
        }

        private int GetNewPointsValue(Player player, NewRoundModel newRoundData)
        {
            if(Game.AscendentPontuation)
                return player.Points.Last() + newRoundData.HeartsPoints + (newRoundData.WinSpadesQueen ? Game.SpadesQueenPointsValue : 0);
            else
                return player.Points.Last() - newRoundData.HeartsPoints + (newRoundData.WinSpadesQueen ? Game.SpadesQueenPointsValue : 0);
        }

        public bool CanAddRound()
        {
            CanAddNewRound = _newRoundModelList.Any(item => item.WinSpadesQueen) && !_newRoundModelList.Any(item => item.HeartsPoints == -1);
            return CanAddNewRound;
        }
    }
}

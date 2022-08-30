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
        private ObservableCollection<string> _rounds;

        [ObservableProperty]
        private List<int> _heartsPointsPossibleValues;
        
        public GameViewModel(IGameDbManagerService gameDbManagerService,
                             IBottomSheetDialogService bottomSheetDialogService)
        {
            _gameDbManagerService = gameDbManagerService;
            _bottomSheetDialogService = bottomSheetDialogService;

            Game = _gameDbManagerService.GetCurrentGame();
            Title = Game.GameName;
            AddRoundBtnText = "Add Round";
            Players = Game.PlayerList;

            var rounds = new ObservableCollection<string>();
            Game.PlayerList[0].Points.ToList().ForEach(round => rounds.Add(string.Format("#{0}", rounds.Count)));
            Rounds = rounds;
            HeartsPointsPossibleValues = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
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
            foreach(var newRoundData in NewRoundModelList)
            {
                var player = newRoundData.Player;
                player.Points.Add(GetNewPointsValue(player, newRoundData));
            }
            Rounds.Add(string.Format("#{0}", Rounds.Count));
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
                await Shell.Current.GoToAsync(nameof(FinishedGamePage), true);
            }
            else if(!Game.AscendentPontuation && minValue < Game.EndScoreValue)
            {
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

    public partial class NewRoundModel : ObservableObject
    {
        private IRelayCommand _addRoundCommand;

        public Action<int> ClearSpadesQueenSelection { get; }

        public NewRoundModel(Player player, Action<int> clearSpadesQueenSelection, IRelayCommand addRoundCommand)
        {
            _addRoundCommand = addRoundCommand;
            Player = player;
            ClearSpadesQueenSelection = clearSpadesQueenSelection;
        }

        [ObservableProperty]
        public Player player;

        [ObservableProperty]
        public int heartsPoints = -1;

        [ObservableProperty]
        public bool winSpadesQueen;

        partial void OnWinSpadesQueenChanged(bool value)
        {
            _addRoundCommand.CanExecute(null);

            if (!value)
                return;
            ClearSpadesQueenSelection?.Invoke(Player.PlayerNumber);
        }

        partial void OnHeartsPointsChanged(int value)
        {
            _addRoundCommand.CanExecute(null);
        }
    }
}

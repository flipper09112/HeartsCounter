using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Models;
using HeartsCounter.Models.Games;
using HeartsCounter.Services.Implementations.Games;
using HeartsCounter.Services.Interfaces.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.CurrentGame
{
    public partial class FinishedGameViewModel : BaseViewModel
    {
        private IGameDbManagerService _gameDbManagerService;

        [ObservableProperty]
        private Game _game;

        [ObservableProperty]
        private Player _winnerPlayer;

        [ObservableProperty]
        private Player _loserPlayer;

        public FinishedGameViewModel(IGameDbManagerService gameDbManagerService)
        {
            _gameDbManagerService = gameDbManagerService;

            Game = _gameDbManagerService.GetCurrentGame();
            Game.GameEnded = true;
            _gameDbManagerService.SaveGame(Game);

            WinnerPlayer = GetWinner();
            LoserPlayer = GetLoser();
        }

        private Player GetLoser()
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

            if (Game.AscendentPontuation)
            {
                return Game.PlayerList.Find(player => player.Points.Max() == minValue);
            }
            else
            {
                return Game.PlayerList.Find(player => player.Points.Max() == maxValue);
            }
        }

        private Player GetWinner()
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

            if (Game.AscendentPontuation)
            {
                return Game.PlayerList.Find(player => player.Points.Max() == maxValue);
            }
            else
            {
                return Game.PlayerList.Find(player => player.Points.Max() == minValue);
            }
        }

        [RelayCommand]
        public void EndGame()
        {
            Shell.Current.GoToAsync("../..", true);
        }
    }
}

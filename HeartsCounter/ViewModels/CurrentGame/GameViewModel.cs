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
        private string _title;

        [ObservableProperty]
        private string _addRoundBtnText;

        [ObservableProperty]
        private Game _game;

        [ObservableProperty]
        private List<string> _rounds;

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

            Rounds = new List<string>() { "#0" };
            HeartsPointsPossibleValues = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        }

        internal async void ShowBottomSheet(Page page, bool dimDismiss)
        {
            //await Shell.Current.GoToAsync(nameof(AddRoundBottomSheetView), true);
            var view = new AddRoundBottomSheetView(this);
            _bottomSheetDialogService.ShowBottomSheet(page, view, dimDismiss);
        }

        [RelayCommand]
        public void AddRoundCommand()
        {
        }
    }
}

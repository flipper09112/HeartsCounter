using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Models.Games
{
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

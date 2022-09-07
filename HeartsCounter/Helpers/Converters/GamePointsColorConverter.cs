using HeartsCounter.Models;
using HeartsCounter.Models.Games;
using HeartsCounter.ViewModels.CurrentGame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Helpers.Converters
{
    public class GamePointsColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AppTheme currentTheme = (AppTheme)Application.Current.RequestedTheme;

            GameViewModel vm = (GameViewModel)value;
            RoundData lastRound = (RoundData)vm.Rounds.Last();
            var label = (Label)parameter;

            if (value == null || string.IsNullOrEmpty(label.Text))
                return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;

            if (label.Text != lastRound.MinRoundValue.ToString() && label.Text != lastRound.MaxRoundValue.ToString())
                return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;
            else if (lastRound.MinRoundValue == lastRound.MaxRoundValue)
                return Colors.Yellow;
            else if (label.Text == lastRound.MinRoundValue.ToString())
                return Colors.Green;
            else if (label.Text == lastRound.MaxRoundValue.ToString())
                return Colors.Red;

            return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

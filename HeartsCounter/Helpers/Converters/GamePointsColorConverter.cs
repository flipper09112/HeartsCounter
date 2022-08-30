using HeartsCounter.Models;
using System;
using System.Collections.Generic;
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

            if (value == null)
                return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;

            CollectionView collectionView = (CollectionView)parameter;
            List<Player> itemSource = (List<Player>)collectionView.ItemsSource;

            int maxValue = int.MinValue;
            int minValue = int.MaxValue;

            itemSource.ForEach(player =>
            {
                var points = player.Points.Last();

                if (points > maxValue)
                    maxValue = points;

                if (points < minValue)
                    minValue = points;
            });

            string labelText = (string)value;

            if (labelText != minValue.ToString() && labelText != maxValue.ToString())
                return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;
            else if (minValue == maxValue)
                return Colors.Yellow;
            else if (labelText == minValue.ToString())
                return Colors.Green;
            else if (labelText == maxValue.ToString())
                return Colors.Red;

            return currentTheme == AppTheme.Light ? Colors.Black : Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

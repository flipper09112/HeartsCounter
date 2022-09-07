using HeartsCounter.Models.Games;
using HeartsCounter.ViewModels.CurrentGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Templates
{
    public class ScoreCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ScoreHeaderTemplate { get; set; }
        public DataTemplate NormalRoundLabelTemplate { get; set; }
        public DataTemplate LastRoundLabelTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is RoundHeader)
                return ScoreHeaderTemplate;

            var roundData = (RoundData) item;
            var viewModel = (GameViewModel)container.BindingContext;

            if ((viewModel.Rounds.Count - 2) == roundData.RoundNumberValue)
                return LastRoundLabelTemplate;

            return NormalRoundLabelTemplate;
        }
    }
}

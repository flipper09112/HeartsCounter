using HeartsCounter.Models.Games;
using HeartsCounter.ViewModels.CurrentGame;
using HeartsCounter.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.TemplateSelectors
{
    public class GameHistoryItemSelector : DataTemplateSelector
    {
        public DataTemplate DateTemplate { get; set; }
        public DataTemplate GameHistoryTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            TimelineItem model = (TimelineItem)item;

            return model.TimelineItemType == TimelineItemTypeEnum.Date ? DateTemplate : GameHistoryTemplate;
        }
    }
}

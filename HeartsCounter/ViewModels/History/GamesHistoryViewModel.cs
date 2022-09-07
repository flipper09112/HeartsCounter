using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeartsCounter.Helpers.Game;
using HeartsCounter.Models.Games;
using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Services.Interfaces.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.ViewModels.History
{
    public partial class GamesHistoryViewModel : BaseViewModel
    {
        private IGameDbManagerService _gameDbManagerService;

        [ObservableProperty]
        private List<GameFilterModel> _gamesFilterList;

        [ObservableProperty]
        private List<DatesIntervalFilterModel> _datesIntervalFilterList;

        [ObservableProperty]
        private List<TimelineItem> _gamesList = new List<TimelineItem>();

        private DatesIntervalFilterModel _datesIntervalSelected;
        private GameFilterModel _gameFilterSelected;
        private List<Game> _filteredList;

        public GamesHistoryViewModel(IGameDbManagerService gameDbManagerService)
        {
            _gameDbManagerService = gameDbManagerService;

            _filteredList = _gameDbManagerService.GetAllGames();
            GamesList = GetTimeline();
            GamesFilterList = GetGamesFilterList();
            DatesIntervalFilterList = GetDatesIntervalFilterList();
        }

        private List<TimelineItem> GetTimeline()
        {
            DateTime? lastYear = null;

            List<TimelineItem> list = new List<TimelineItem>();
            foreach (var game in _filteredList)
            {
                if(lastYear == null || lastYear.Value.Year != game.StartDate.Year)
                {
                    lastYear = game.StartDate;
                    list.Add(new TimelineItem()
                    {
                        TimelineItemType = TimelineItemTypeEnum.Date,
                        DateFormatted = lastYear.Value.Year.ToString()
                    });
                }

                list.Add(new TimelineItem()
                {
                    TimelineItemType = TimelineItemTypeEnum.Game,
                    Game = game
                });
            }

            return list;
        }

        [RelayCommand]
        public void SelectGame(Game game)
        {
            IsBusy = true;
            _gameDbManagerService.SelectedHistoryGame = game;
            Shell.Current.GoToAsync(nameof(GamePage));
            IsBusy = false;
        }

        [RelayCommand]
        public void SelectDateIntervalFilterList(DatesIntervalFilterModel datesIntervalFilterModel)
        {
            _datesIntervalSelected = datesIntervalFilterModel;
            DatesIntervalFilterList.ForEach(item => item.Selected = false);
            datesIntervalFilterModel.Selected = true;
            FilterList();
        }

        [RelayCommand]
        public void SelectGameFilter(GameFilterModel gameFilterModelSelected)
        {
            _gameFilterSelected = gameFilterModelSelected;
            GamesFilterList.ForEach(item => item.Selected = false);
            gameFilterModelSelected.Selected = true;
            FilterList();
        }

        private void FilterList()
        {
            IsBusy = true;

            _filteredList = _gameDbManagerService.GetAllGames();

            FilterByDate();
            FilterByGame();

            GamesList = GetTimeline();

            IsBusy = false;
        }

        private void FilterByGame()
        {
            var gameFilterSelected = GamesFilterList.Find(item => item.Selected);

            if (gameFilterSelected.GameType == GameTypeEnum.All)
                return;

            _filteredList = _filteredList.Where(game => game.GameType == gameFilterSelected.GameType).ToList();
        }

        private void FilterByDate()
        {
            var dateFilterSelected = DatesIntervalFilterList.Find(item => item.Selected);

            if (dateFilterSelected.DateIntervalType == DateIntervalTypeEnum.All)
                return;

            switch(dateFilterSelected.DateIntervalType)
            {
                case DateIntervalTypeEnum.Last_Month:
                    _filteredList = _filteredList.Where(game => game.StartDate > game.StartDate.AddMonths(-1)).ToList();
                    return;
                case DateIntervalTypeEnum.Last_3_Months:
                    _filteredList = _filteredList.Where(game => game.StartDate > game.StartDate.AddMonths(-3)).ToList();
                    return;
                case DateIntervalTypeEnum.Last_6_Months:
                    _filteredList = _filteredList.Where(game => game.StartDate > game.StartDate.AddMonths(-6)).ToList();
                    return;
                case DateIntervalTypeEnum.Last_Year:
                    _filteredList = _filteredList.Where(game => game.StartDate > game.StartDate.AddYears(-1)).ToList();
                    return;
            }
        }

        private List<DatesIntervalFilterModel> GetDatesIntervalFilterList()
        {
            List<DatesIntervalFilterModel> datesIntervalFilter = new List<DatesIntervalFilterModel>();

            datesIntervalFilter.Add(new DatesIntervalFilterModel()
            {
                DateDescription = "All",
                DateIntervalType = DateIntervalTypeEnum.All,
                Selected = true
            });
            datesIntervalFilter.Add(new DatesIntervalFilterModel()
            {
                DateDescription = "Last Month",
                DateIntervalType = DateIntervalTypeEnum.Last_Month
            });
            datesIntervalFilter.Add(new DatesIntervalFilterModel()
            {
                DateDescription = "Last 3 Months",
                DateIntervalType = DateIntervalTypeEnum.Last_3_Months
            });
            datesIntervalFilter.Add(new DatesIntervalFilterModel()
            {
                DateDescription = "Last 6 Months",
                DateIntervalType = DateIntervalTypeEnum.Last_6_Months
            }); 
            datesIntervalFilter.Add(new DatesIntervalFilterModel()
            {
                DateDescription = "Last Year",
                DateIntervalType = DateIntervalTypeEnum.Last_Year
            });

            return datesIntervalFilter;
        }

        private List<GameFilterModel> GetGamesFilterList()
        {
            List<GameFilterModel> gameFilter = new List<GameFilterModel>();
            
            foreach (GameTypeEnum gameType in Enum.GetValues(typeof(GameTypeEnum)))
            {
                gameFilter.Add(new GameFilterModel()
                {
                    GameType = gameType,
                    Image = GamesHelper.GetImageGame(gameType),
                    Name = gameType.ToString(),
                    Selected = gameType == GameTypeEnum.All
                });
            }

            return gameFilter;
        }

       
    }

    public class TimelineItem
    {
        public TimelineItemTypeEnum TimelineItemType { get; set; }
        public string DateFormatted { get; set; }
        public Game Game { get; set; }
        public string Icon => GamesHelper.GetImageGame(Game.GameType);
    }

    public enum TimelineItemTypeEnum
    {
        Date,
        Game
    }

    public partial class DatesIntervalFilterModel : ObservableObject
    {
        public string DateDescription { get; internal set; }
        public DateIntervalTypeEnum DateIntervalType { get; internal set; }

        public Color BackGroundColor => Selected ? Colors.White : Colors.Transparent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(BackGroundColor))]
        private bool _selected;
    }

    public enum DateIntervalTypeEnum
    {
        All,
        Last_Month,
        Last_3_Months,
        Last_6_Months,
        Last_Year
    }

    public partial class GameFilterModel : ObservableObject
    {
        public GameTypeEnum GameType { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

        public Color BackGroundColor => Selected ? Colors.White : Colors.Transparent;

        [ObservableProperty]
        private bool _selected;
    }
}

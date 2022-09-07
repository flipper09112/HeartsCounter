using HeartsCounter.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Helpers.Game
{
    public static class GamesHelper
    {
        public static string GetImageGame(GameTypeEnum gameType)
        {
            switch (gameType)
            {
                case GameTypeEnum.Copas:
                    return "ic_heart";
                case GameTypeEnum.Sueca:
                    return "ic_sueca";
                case GameTypeEnum.Buraco:
                    return "ic_hole";
                case GameTypeEnum.Canastra:
                    return "ic_carroca";
                case GameTypeEnum.All:
                default:
                    return "heart_counter_logo";
            }
        }
    }
}

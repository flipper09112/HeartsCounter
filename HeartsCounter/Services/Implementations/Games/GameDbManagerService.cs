using HeartsCounter.Models.Games;
using HeartsCounter.Services.Implementations.Database;
using HeartsCounter.Services.Interfaces.Database;
using HeartsCounter.Services.Interfaces.Games;
using Java.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace HeartsCounter.Services.Implementations.Games
{
    public class GameDbManagerService : IGameDbManagerService
    {
        private IDatabaseManagerService _databaseManagerService;

        public GameDbManagerService(IDatabaseManagerService databaseManagerService)
        {
            _databaseManagerService = databaseManagerService;
        }

        public bool AddNewGame(Game newGame)
        {
            // enter this line
            var result = _databaseManagerService.SQLConnetion.Insert(newGame);

            return result != 0;
        }

        public List<Game> GetAllGames()
        {
            try
            {
                return _databaseManagerService.SQLConnetion.Table<Game>().ToList();
            }
            catch (Exception ex)
            {
                //StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Game>();
        }
    }
}

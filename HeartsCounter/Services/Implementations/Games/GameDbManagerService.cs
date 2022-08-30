using HeartsCounter.Models.Games;
using HeartsCounter.Services.Implementations.Database;
using HeartsCounter.Services.Interfaces.Database;
using HeartsCounter.Services.Interfaces.Games;

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
                Console.WriteLine(ex.Message);
            }

            return new List<Game>();
        }

        public Game GetCurrentGame()
        {
            var list = _databaseManagerService.SQLConnetion.Table<Game>().ToList();

            return list.Count == 0 ? null : list.First();
        }

        public void SaveGame(Game game)
        {
            _databaseManagerService.SQLConnetion.Update(game);
        }
    }
}

using HeartsCounter.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Services.Interfaces.Games
{
    public interface IGameDbManagerService
    {
        Game SelectedHistoryGame { get; set; }

        bool AddNewGame(Game newGame);

        Game GetCurrentGame();
        List<Game> GetAllGamesFinished();
        List<Game> GetAllGames();

        void SaveGame(Game game);
    }
}

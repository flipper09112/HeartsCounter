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
        bool AddNewGame(Game newGame);

        Game GetCurrentGame();

        List<Game> GetAllGames();
    }
}

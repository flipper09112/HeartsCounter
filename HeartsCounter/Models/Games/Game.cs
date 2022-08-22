using SQLite;
using System.Text.Json;

namespace HeartsCounter.Models.Games;

[Table("Game")]
public class Game
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250), Unique]
    public string GameName { get; set; }

    [Ignore]
    public List<Player> PlayerList { get; set; }

    public string PlayerListJson => JsonSerializer.Serialize(PlayerList);

    public bool AscendentPontuation { get; set; }

    public bool GameEnded { get; set; }

    //COPAS

    public int SpadesQueenPointsValue { get; set; }

    public int EndScoreValue { get; set; }
}

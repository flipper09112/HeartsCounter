using SQLite;
using System.Text.Json;

namespace HeartsCounter.Models.Games;

[Table("Game")]
public class Game
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250)]
    public string GameName { get; set; }

    private List<Player> _playerList;

    [Ignore]
    public List<Player> PlayerList
    {
        get
        {
            if(_playerList == null)
                _playerList = JsonSerializer.Deserialize<List<Player>>(_playerListJson);

            return _playerList;
        }
        set
        {
            _playerList = value;
        }
    }

    private string _playerListJson;
    public string PlayerListJson
    {
        get => JsonSerializer.Serialize(PlayerList ?? new List<Player>());
        set
        {
            _playerListJson = value;
        }
    } 

    public bool AscendentPontuation { get; set; }

    public bool GameEnded { get; set; }

    //COPAS

    public int SpadesQueenPointsValue { get; set; }

    public int EndScoreValue { get; set; }
}

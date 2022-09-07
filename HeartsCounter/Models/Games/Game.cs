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

    public GameTypeEnum GameType { get; set; }

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
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }

    //COPAS
    public int SpadesQueenPointsValue { get; set; }
    public int EndScoreValue { get; set; }

    [Ignore]
    public Player Winner
    {
        get
        {
            Player winner = null;

            PlayerList.ForEach(player =>
            {
                if (winner == null)
                {
                    winner = player;
                }
                else
                {
                    if (AscendentPontuation)
                    {
                        int max = winner.Points.Max();
                        if (player.Points.Max() > max)
                            winner = player;
                    }
                    else
                    {
                        int min = winner.Points.Min();
                        if (player.Points.Min() < min)
                            winner = player;
                    }
                }
            });

            return winner;
        }
    }

    [Ignore]
    public int WinnerPoints => AscendentPontuation ? Winner.Points.Max() : Winner.Points.Min();

    [Ignore]
    public int Duration => Math.Abs((FinishDate - StartDate).Minutes);
}

public enum GameTypeEnum
{
    All,
    Copas,
    Sueca,
    Canastra,
    Buraco
}
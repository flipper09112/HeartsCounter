using CommunityToolkit.Mvvm.ComponentModel;

namespace HeartsCounter.Models;

public class Player : ICloneable
{
    public Player() { }

    public Player(Player player)
    {
        PlayerNumber = player.PlayerNumber;
        Name = player.Name;
        PlaceHolder = player.PlaceHolder;
    }

    public int PlayerNumber { get; set; }
    public string Name { get; set; }
    public string PlaceHolder { get; set; } = "Player name";

    public object Clone()
    {
        return new Player(this);
    }
}

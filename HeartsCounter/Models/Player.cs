using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace HeartsCounter.Models;

public partial class Player : ObservableObject, ICloneable
{
    public Player() { }

    public Player(Player player)
    {
        PlayerNumber = player.PlayerNumber;
        Name = player.Name;
        PlaceHolder = player.PlaceHolder;
    }

    [ObservableProperty]
    public int playerNumber;

    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public string placeHolder = "Player name";

    [ObservableProperty]
    public ObservableCollection<int> points = new ObservableCollection<int>() { 0 };

    public object Clone()
    {
        return new Player(this);
    }
}

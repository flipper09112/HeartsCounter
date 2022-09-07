using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Models.Games
{
    public class Round
    {
    }

    public class RoundData : Round
    {
        public string RoundNumber { get; set; }
        public int RoundNumberValue { get; set; }

        public int MaxRoundValue => RoundPoints.Max();
        public int MinRoundValue => RoundPoints.Min();

        public List<int> RoundPoints { get; set; } = new List<int>();
    }

    public class RoundHeader : Round
    {
        public string RoundHeaderTitle { get; set; }

        public List<Player> Players { get; set; }
    }
}

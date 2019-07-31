namespace Simulator
{
    public class PlayerStats
    {
        public int AtBats { get; set; }
        public int Walks { get; set; }
        public int Strikeouts { get; set; }
        public int Singles { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }

        public int Hits => Singles + Doubles + Triples + HomeRuns;

        public int TotalBases => Singles + (Doubles * 2) + (Triples * 3) + (HomeRuns * 4);

        public double BattingAverage => Hits/ (double)AtBats;

        public double OnBasePercentage => (Walks + Hits) / (double)AtBats;

        public double Slugging => TotalBases / (double)AtBats;

        public double GrossProductionAverage => ((1.8 * OnBasePercentage) + Slugging) / 4.0;

        public double IsolatedPower => Slugging - BattingAverage;

    }
}

using System;

namespace Simulator
{
    public class Team
    {
        private int atBat;
        private Player[] roster;

        public Player UpToBat => roster[atBat];

        public string Name { get; set; }

        public void NextHitter()
        {
            atBat = (atBat + 1) % roster.Length;
        }

        public Team(string name, Player[] roster)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.roster = roster ?? throw new ArgumentNullException(nameof(roster));
            atBat = 0;
        }
    }
}

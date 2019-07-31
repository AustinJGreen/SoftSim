namespace Simulator
{
    class Game
    {
        private int halfInnings;
        private Team home, away;
        private InningState state;

        public Team GetHittingTeam()
        {
            return halfInnings % 2 == 0 ? away : home;
        }

        public Team GetFieldingTeam()
        {
            return halfInnings % 2 == 0 ? home : away;
        }

        public void EnterHalfInning(Play[] plays)
        {
            InningState start = new InningState();

            // Simulate plays, find possibilities for missing plays
            Team hittingTeam = GetHittingTeam();   
            for (int i = 0; i < plays.Length; i++)
            {
                Player atBat = hittingTeam.UpToBat;
                
            }

            halfInnings += 1;
        }

        public void FindOptimalLineup()
        {
            // Simulate all permutations of lineup using stats to see what scores the most runs
            // - Tune hyperparameters to games already played 2 x (6 to 24 scores)
            //  For each 7 innings
            //      Go through lineup
            //          On each batter, take random number between 0..1
            //          If number <= BA -> hit is decided
            //              If number <= HR / Hits -> hitter hits a homerun
            //              If number <= 3B / Hits -> hitter hits a triple
            //              If number <= 2B / Hits -> hitter hits a double
            //              If number <= 1B / Hits -> hitter hits a single
            //          Else If number <= OBP, hitter walks
            //          Else 
            //              Draw another number between 0 and 1
            //              If number <= SO / OUTS -> hitter strikesout (out added)
            //              Else
            //                  Hitter hits the ball into play, simulate fielding using slgging and current base runners 
            //                  ... can also include opposing team skill level for ability to catch balls, turn double plays
            
            
        }

        public Game(Team home, Team away)
        {
            this.home = home;
            this.away = away;
            halfInnings = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreTracker
{
    internal class BowlingGame
    {
        internal IList<Bowler> Bowlers { get; }
        public ScoreBoard Score { get; set; }

        public BowlingGame(List<Bowler> bowlers)
        {
            Bowlers = bowlers;
            Score = new ScoreBoard();
        }

        internal static void StartGame(List<Bowler> bowlers)
        {
            for (int round = 1; round < 10; round++)
            {
                foreach (Bowler bowler in bowlers)
                {
                    int standingPins = 10;
                    for (int ball = 0; ball < 2; ball++)
                    {
                        int knockedDownPins = bowler.Roll(standingPins);
                        standingPins -= knockedDownPins;

                        Score.Track(bowler, round, ball, knockedDownPins);

                        if (ball == 2 || standingPins == 0)
                        {
                            break;
                        }
                    }
                }
            }
        }

        

    }
}




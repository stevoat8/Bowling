using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    internal class Game
    {
        internal Bowler Bowler { get; }
        private ScoreBoard ScoreBoard;

        internal Game(Bowler bowler)
        {
            Bowler = bowler;
            ScoreBoard = new ScoreBoard();
        }

        internal void Play()
        {
            for (int frame = 1; frame <= 10; frame++)
            {
                int standingPins = 10;
                for (int ball = 1; ball <= 3; ball++)
                {
                    int knockedDownPins = Bowler.Roll(standingPins);
                    standingPins -= knockedDownPins;

                    ScoreBoard.Track(frame, ball, knockedDownPins);

                    if (frame == 10 && standingPins == 0)
                    {
                        standingPins = 10;
                    }
                    else if (ball == 2 || standingPins == 0)
                    {
                        break;
                    }
                }
            }
        }

        internal int GetFinalScore()
        {
            return ScoreBoard.GetScore(10);
        }

        internal void GetAllScores()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("#" + i + ":");
                int score = ScoreBoard.GetScore(i);
                Console.WriteLine("____________________");
            }
        }
    }
}




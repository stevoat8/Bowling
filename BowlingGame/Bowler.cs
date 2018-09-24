using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bowling
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal class Bowler
    {
        private static Random rdm = new Random();
        private ScoreBoard scoreBoard;

        /// <summary>
        /// Representation of a Bowler.
        /// </summary>
        private string DebuggerDisplay
        {
            get { return Nr + " - " + Name; }
        }
        internal int Nr { get; private set; }
        internal string Name { get; set; }

        internal Bowler(int nr, string name)
        {
            Nr = nr;
            Name = name;
            scoreBoard = new ScoreBoard();
        }

        internal void Play()
        {
            for (int frame = 1; frame <= 10; frame++)
            {
                int standingPins = 10;
                for (int ball = 1; ball <= 3; ball++)
                {
                    int knockedDownPins = Roll(standingPins);
                    standingPins -= knockedDownPins;

                    scoreBoard.Track(frame, ball, knockedDownPins);

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

        /// <summary>
        /// Rolls a bowling ball and returns the number of knocked down pins (0-10).
        /// </summary>
        /// <param name="pinsStanding">Number of pins still standing.</param>
        /// <returns>Number of knocked down pins.</returns>
        internal int Roll(int pinsStanding)
        {
            int knockedDownPins = rdm.Next(pinsStanding + 1);
            knockedDownPins = 10; //Always strikes
            return knockedDownPins;
        }

        internal int GetFinalScore()
        {
            return scoreBoard.GetFrameScore(10);
        }

        internal void PrintAllScores()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"#{Nr} {Name}");
                Console.WriteLine("     1  2  3");
                Console.WriteLine("---------------");
                int score = scoreBoard.GetFrameScore(i);
                Console.WriteLine("_______________\n");
            }
        }
    }
}
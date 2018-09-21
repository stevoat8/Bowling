using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScoreTracker
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal class Bowler
    {
        static Random rdm = new Random();

        private List<FramesEnum> trackedFrames;

        /// <summary>
        /// Representation of a Bowler.
        /// </summary>
        private string DebuggerDisplay
        {
            get { return Nr + "|" + Name; }
        }
        public int Nr { get; private set; }
        public string Name { get; set; }

        public Bowler(int nr, string name)
        {
            Nr = nr;
            Name = name;
            trackedFrames = new List<FramesEnum>();
        }

        /// <summary>
        /// Rolls a bowling ball and returns the number of knocked down pins (0-10).
        /// </summary>
        /// <param name="pinsStanding">Number of pins still standing.</param>
        /// <returns>Number of knocked down pins.</returns>
        public int Roll(int pinsStanding)
        {
            int knockedDownPins = rdm.Next(pinsStanding + 1);
            knockedDownPins = 10;
            Console.Write("Wie viele Pins getroffen? ");
            knockedDownPins = int.Parse(Console.ReadLine());

            return knockedDownPins;
        }

        /// <summary>
        /// Calculates the bowler's score after the passed round.
        /// </summary>
        /// <param name="round"></param>
        /// <returns>The bowler's score at the end of the passed round.</returns>
        internal int GetScore()
        {
            int totalScore = 0;

            for (int i = 0; i < trackedFrames.Count; i++)
            {
                //If the bowler scored a strike in this round, the score of the next 2 rounds
                //are added to this round's score. A Spare adds only the score of the next round.
                int additionallyEvaluatedRounds = (int)trackedFrames[i] % 9; //0, 1 oder 2
                int totalRoundScore = 0;
                for (int j = 0; j <= additionallyEvaluatedRounds && i + j < trackedFrames.Count; j++)
                {
                    //Get real value from FrameEnum
                    int frameScore = ((int)trackedFrames[i + j] > 10)
                        ? 10
                        : (int)trackedFrames[i + j];
                    totalRoundScore += frameScore;
                }
                totalScore += totalRoundScore;
            }

            return totalScore;
        }

        internal void TrackFrame(FramesEnum frame)
        {
            trackedFrames.Add(frame);
        }
    }
}
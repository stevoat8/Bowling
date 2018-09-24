using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    internal class ScoreBoard
    {
        private readonly int[][] scores;

        internal ScoreBoard()
        {
            scores = new int[10][];
            for (int i = 0; i < 9; i++)
            {
                scores[i] = new int[2];
            }
            scores[9] = new int[3];
        }

        internal void Track(int frame, int ball, int knockedDownPins)
        {
            scores[frame - 1][ball - 1] = knockedDownPins;
        }

        internal int GetFrameScore(int frameNr)
        {
            int finalScore = 0;
            for (int frameIndex = 0; frameIndex < frameNr; frameIndex++)
            {
                int frameScore = GetBaseFrameScore(frameIndex);

                int extraEvaluatedFrames = GetExtraEvaluatedFramesCount(frameNr, frameIndex, frameScore);

                for (int extraFrame = 1; extraFrame <= extraEvaluatedFrames; extraFrame++)
                {
                    if (frameIndex == 9 || (frameIndex == 8 && extraFrame == 2))
                    {
                        frameScore += scores[9][extraFrame];
                    }
                    else
                    {
                        frameScore += GetBaseFrameScore(frameIndex + extraFrame);
                    }
                }
                PrintIntermediateScore(frameIndex, frameScore);
                finalScore += frameScore;
            }
            PrintFinalScore(finalScore);
            return finalScore;
        }

        private int GetBaseFrameScore(int frameIndex)
        {
            int baseScore = 0;
            if (frameIndex == 9 && scores[9][0] == 10)
            {
                baseScore = 10;
            }
            else
            {
                baseScore = scores[frameIndex][0] + scores[frameIndex][1];
            }
            return baseScore;
        }

        private int GetExtraEvaluatedFramesCount(int frameNr, int frameIndex, int frameScore)
        {
            int extraEvaluatedFrames = 0;
            if (scores[frameIndex][0] == 10) // Strike
            {
                extraEvaluatedFrames = 2;
            }
            else if (frameScore == 10) // Spare
            {
                extraEvaluatedFrames = 1;
            }

            //If additionally Frames would be evaluated, but their indexes would be out of bound, they are not evaluated.
            //Exception #1: The extra frames for frame #10 are the frame's own indexes.
            //Exception #2: The 2nd extra frame for frame #9 is the 3rd Index of frame #10.
            if ((frameNr == 10 || (frameNr == 10 && frameIndex == 8 && extraEvaluatedFrames == 2)) == false)
            {
                extraEvaluatedFrames = Math.Min(frameNr - 1 - frameIndex, extraEvaluatedFrames);
            }

            return extraEvaluatedFrames;
        }

        private void PrintIntermediateScore(int frameIndex, int totalFrameScore)
        {
            int frameNr = frameIndex + 1;
            string ball1Score = scores[frameIndex][0].ToString();
            string ball2Score = scores[frameIndex][1].ToString();
            string ball3Score = frameIndex == 9 ? scores[9][2].ToString() : "";

            Console.WriteLine("{0,2}: {1,2} {2,2} {3,2} {4,2}",
                                frameNr,
                                ball1Score,
                                ball2Score,
                                ball3Score,
                                totalFrameScore);
        }

        private void PrintFinalScore(int finalScore)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("     Total: {0,3}", finalScore);
        }
    }
}
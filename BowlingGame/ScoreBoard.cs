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

        internal int GetScore(int lastFrame)
        {
            int finalScore = 0;
            for (int frameIndex = 0; frameIndex < lastFrame; frameIndex++)
            {
                int baseFrameScore = GetBaseFrameScore(frameIndex);

                int extraFrames = 0; //Additionally evaluated frames
                if (scores[frameIndex][0] == 10) // Strike
                {
                    extraFrames = 2;
                }
                else if (baseFrameScore == 10) // Spare
                {
                    extraFrames = 1;
                }

                int extraFrameScore = 0;
                for (int extraFrame = 1; extraFrame <= extraFrames; extraFrame++)
                {
                    int extraFrameIndex = frameIndex + extraFrame;
                    if (frameIndex == 8 && lastFrame == 9)
                    {
                        break;
                    }
                    else if (extraFrameIndex < lastFrame || (frameIndex == 8 && extraFrame == 1))
                    {
                        extraFrameScore += GetBaseFrameScore(extraFrameIndex);
                    }
                    else if (frameIndex == 9 || (frameIndex == 8 && extraFrame == 2)) //last frame
                    {
                        extraFrameScore += scores[9][extraFrame];
                    }
                }

                int totalFrameScore = baseFrameScore + extraFrameScore;
                finalScore += totalFrameScore;

                PrintIntermediateScore(frameIndex, totalFrameScore);
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
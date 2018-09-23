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
            //TODO: Spares werden eventuell nicht richtig gewertet! Debuggen
            int finalScore = 0;
            for (int frameIndex = 0; frameIndex < lastFrame; frameIndex++)
            {
                int baseFrameScore = GetBaseFrameScore(frameIndex);

                int extraFrames = 0; //Additionally evaluated frames
                if (scores[frameIndex][0] == 10)
                {
                    extraFrames = 2; // Strike
                }
                else if (scores[frameIndex][1] == 10)
                {
                    extraFrames = 1; // Spare
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
                Console.WriteLine("{0,2}: {1}", frameIndex + 1, totalFrameScore);
                finalScore += totalFrameScore;
            }
            Console.WriteLine($"-------\n    {finalScore}");
            return finalScore;
        }

        private int GetBaseFrameScore(int frameIndex)
        {
            int baseScore = 0;
            if (frameIndex == 9)
            {
                if (scores[9][0] == 10)
                {
                    baseScore = 10;
                }
                else
                {
                    baseScore = scores[9][0] + scores[9][1]; ;
                }
            }
            else
            {
                baseScore = scores[frameIndex].Sum();
            }
            return baseScore;
        }
    }
}
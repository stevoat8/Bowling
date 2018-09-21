using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreTracker
{
    internal class BowlingGame
    {
        internal void Go()
        {
            //List<Bowler> bowlers = StartRegistration();

            //Console.Clear();
            //ShowBowlers(bowlers);

            //Console.Write("\nPress Enter to start game");
            //Console.ReadKey(true);
            //Console.Clear();

            //-------------------

            //Test-Bowler to skip registration while developing
            //List<Bowler> bowlers = bowlers = new List<Bowler>() {
            //    new Bowler(1,"Body"),
            //    new Bowler(2,"Doyle"),
            //    new Bowler(3,"Tiger"),
            //    new Bowler(4,"Jewls")
            //};
            //Start(bowlers);

            //-------------------

            //Test Score calculation for only one bowler
            Start(new Bowler(1, "Doyle"));

            Console.ReadKey();
        }

        private static void Start(Bowler bowler)
        {
            for (int round = 1; round <= 10; round++)
            {
                Console.WriteLine("Round #{0}", round);
                int bonusBall = 0;
                int maxBalls = 2;
                int standingPins = 10;
                for (int ball = 1; ball <= maxBalls; ball++)
                {
                    int knockedDownPins = bowler.Roll(standingPins);
                    standingPins -= knockedDownPins;

                    FramesEnum frame = FramesEnum.Pin0;

                    if (ball == 1)
                    {
                        if (standingPins == 0)
                        {
                            frame = FramesEnum.Strike;
                            if (round < 10)
                            {
                                ball++;
                            }
                            else
                            {
                                //ball--;//bonusBall++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (standingPins == 0)
                        {
                            frame = FramesEnum.Spare;
                        }
                        else
                        {
                            frame = (FramesEnum)knockedDownPins;
                        }
                    }

                    bowler.TrackFrame(frame);
                }
                ShowScore(bowler, round);
            }
        }

        private static void ShowComment(Bowler bowler, int round, int ball, int standingPins)
        {
            Console.Write($"{bowler.Name}'s {ball}. roll. {standingPins} pins still standing. {bowler.Name} rolls... ");

        }

        private static void Start(List<Bowler> bowlers)
        {
            for (int round = 1; round < 10; round++)
            {
                for (int bowler = 0; bowler < bowlers.Count; bowler++)
                {
                    int maxShots = round == 10 ? 3 : 2;

                    for (int shot = 0; shot < maxShots; shot++)
                    {

                    }
                }
            }
        }

        private static void ShowScore(Bowler bowler, int round)
        {
            int score = bowler.GetScore();
            Console.WriteLine($"Score after round {round}: {score}");
        }

        private static void ShowScore(List<Bowler> bowlers, int round)
        {
            Console.WriteLine($"Score round {round}");
            Console.WriteLine("Bowler\tName\tScore");
            foreach (Bowler bowler in bowlers)
            {
                int score = bowler.GetScore();
                Console.WriteLine($"#{bowler.Nr}\t{bowler.Name}\t{score}");
            }
            Console.WriteLine();
        }

        #region Registration
        private static List<Bowler> Registration()
        {
            Console.WriteLine("Registration:");
            List<Bowler> bowlers = new List<Bowler>();

            bool continueRegistration = true;
            int bowlerCount = 1;
            while (continueRegistration)
            {
                Console.Write($"Bowler #{bowlerCount} Name: ");
                string playerNameInput = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(playerNameInput))
                {
                    Console.WriteLine("Invalid bowler Name");
                    continue;
                }
                else
                {
                    bowlers.Add(new Bowler(bowlerCount, playerNameInput));
                }

                continueRegistration = GetDecision("Add another bowler?");
                bowlerCount++;
            }

            Console.WriteLine("Registration finished.");

            return bowlers;
        }

        /// <summary>
        /// Get user decision from Console (Yes = true, no = false).
        /// </summary>
        /// <param name="question">promted Question.</param>
        /// <returns>The User's decision (Yes = true, no = false).</returns>
        private static bool GetDecision(string question)
        {
            bool decision = false;
            bool inputValid = false;
            while (inputValid == false)
            {
                Console.WriteLine(question + " (Y/n) ");
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.KeyChar == 'n' || cki.Key == ConsoleKey.Escape)
                {
                    inputValid = true;
                    decision = false;
                }
                else if (cki.KeyChar == 'y' || cki.Key == ConsoleKey.Enter)
                {
                    inputValid = true;
                    decision = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    inputValid = false;
                }
            }

            return decision;
        }

        private static void ShowBowlers(List<Bowler> bowlers)
        {
            Console.WriteLine("Bowlers:");
            foreach (Bowler bowler in bowlers)
            {
                Console.WriteLine("Bowler #" + bowler.Nr + ": \t" + bowler.Name);
            }
        }
        #endregion
    }

}




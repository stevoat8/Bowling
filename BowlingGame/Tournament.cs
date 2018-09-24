using System;
using System.Collections.Generic;

namespace Bowling
{
    internal static class Tournament
    {
        public static void Start()
        {
            //List<Bowler> bowlers = Registration();
            List<Bowler> bowlers = bowlers = new List<Bowler>() {
                new Bowler(1,"Bodie"),
                new Bowler(2,"Doyle"),
                //new Bowler(3,"Tiger"),
                //new Bowler(4,"Jewls")
            };

            foreach (Bowler bowler in bowlers)
            {
                bowler.Play();
            }

            for (int i = 1; i <= 10; i++)
            {
                foreach (Bowler bowler in bowlers)
                {
                    Console.WriteLine($"Bowler #{bowler.Nr} {bowler.Name}");
                    Console.WriteLine("     1  2  3");
                    Console.WriteLine("---------------");
                    bowler.PrintFrameScore(i);
                    Console.WriteLine("_______________\n");
                }
            }

            Console.ReadKey();
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

        #endregion Registration
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bowling
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal class Bowler
    {
        private static Random rdm = new Random();

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
        }

        /// <summary>
        /// Rolls a bowling ball and returns the number of knocked down pins (0-10).
        /// </summary>
        /// <param name="pinsStanding">Number of pins still standing.</param>
        /// <returns>Number of knocked down pins.</returns>
        internal int Roll(int pinsStanding)
        {
            int knockedDownPins = rdm.Next(pinsStanding + 1);
            return knockedDownPins;
        }
    }
}
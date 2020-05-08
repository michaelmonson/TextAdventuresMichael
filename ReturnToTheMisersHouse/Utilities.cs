using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReturnToTheMisersHouse
{
    public static class Utilities
    {

        /*
         * Once I figure out how to implement it properly, this is shorthand for checking
         * subsets of enumerations.  Rather than a ponderous || piping of each valid case.
         */
        public static bool In<T>(this T val, params T[] values) where T : struct
        {
            return values.Contains(val);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("UnitTestsPoFTerminal")]

namespace PointOfSaleTerminal
{
    public class DiscountRanges
    {
        public Dictionary<int, ulong> discountRanges = new Dictionary<int, ulong>
            {
                { 0 , 999},
                { 1 , 1999},
                { 3 , 4999},
                { 5 , 9999},
                { 7 , ulong.MaxValue},
            };

        public int GetDiscount(double totalSum)
        {
            foreach (var rank in discountRanges)
            {
                if (totalSum <= rank.Value)
                {
                    return rank.Key;
                }
            }
            return 0;
        }
    }
}

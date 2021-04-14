using PointOfSaleTerminal.DataModels;
using System.Collections.Generic;

namespace PointOfSaleTerminal
{
    public class DiscountRanges
    {
        private Dictionary<int, ulong> discountRanges = new Dictionary<int, ulong>
            {
                { 0 , 999},
                { 1 , 1999},
                { 3 , 4999},
                { 5 , 9999},
                { 7 , ulong.MaxValue},
            };

        public int GetDiscount(DiscountCard dCard)
        {
            foreach (var rank in discountRanges)
            {
                if (dCard?.TotalSum <= rank.Value)
                {
                    return rank.Key;
                }
            }
            return 0;
        }
    }
}

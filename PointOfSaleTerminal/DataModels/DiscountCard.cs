using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("UnitTestsPoFTerminal")]

namespace PointOfSaleTerminal.DataModels
{
    public class DiscountCard
    {
        public double Amount { get; set; }
        public int DiscountPercent { get; set; }
        //{
        //    get { return discountPercent; }
        //    set
        //    {
        //        discountPercent = GetPercentRange();
        //        value = discountPercent;
        //    }
        //}

        public DiscountCard()
        {
            Amount = 0;
        }
        public DiscountCard(double AmountPurchases)    //  // need to realize logic of verify customer
        {
            Amount = AmountPurchases;
            //Amount = GetAmount();
            DiscountPercent  = GetPercent(Amount);
        }

        public double GetAmount()   // get amount of successed purchases in dCard
        {
            return 0;
        }

        public int GetPercent(double amount)    // calculate discountPercent via Discount.Amount
        {
            int percent = 0; 
            Ranks ranks = new Ranks();

            foreach (var rank in ranks.vas)
            {
                if (amount >= rank.Value.Start.Value && (amount <= rank.Value.End.Value))
                {
                    percent =  rank.Key;
                }
            }
            return percent;
        }

        //public int DiscountPercent
        //{
        //    get { return discountPercent; }
        //    set
        //    {
        //        //if (Ranges.r0)
        //        var s =  Ranges.r0;
        //        discountPercent = value;
        //    }
        //}

        //public DiscountCard(long _customerId)
        //{
        //    // need to realize logic of verify customer
        //}
    }

    public class Ranks
    {
        public Dictionary<int, System.Range> vas = new Dictionary<int, System.Range>
            {
                { 0 , r0},
                { 1 , r1},
                { 3 , r3},
                { 5 , r5},
                { 7 , r7},
            };
        public static System.Range r0 = ..999;
        public static System.Range r1 = 1000..1999;
        public static System.Range r3 = 2000..4999;
        public static System.Range r5 = 5000..9999;
        public static System.Range r7 = 10000..;
    }
}

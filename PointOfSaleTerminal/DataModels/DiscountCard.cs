using System;
using System.Runtime.CompilerServices;

namespace PointOfSaleTerminal.DataModels
{
    public class DiscountCard
    {
        public string CardId { get; set; }
        public double TotalSum { get; set; }

        public void UpdateCard(DiscountCard card, double sum)
        {
            if (card != null)
            {
                TotalSum += sum;
            }
        }
    }
}

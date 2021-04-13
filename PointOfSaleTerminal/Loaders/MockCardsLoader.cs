using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PointOfSaleTerminal.Loaders
{
    internal class MockCardsLoader : ICardLoader
    {
        private List<DiscountCard> dCardsList = new List<DiscountCard>
        {
            new DiscountCard{  CardId = "0", TotalSum = 0 },
            new DiscountCard{  CardId = "1", TotalSum = 1500 },
            new DiscountCard{  CardId = "2", TotalSum = 3000 },
            new DiscountCard{  CardId = "3", TotalSum = 6000 },
            new DiscountCard{  CardId = "4", TotalSum = 60000 },
        };

        public DiscountCard GetCard(string id)
        {
            return dCardsList.FirstOrDefault(card => card.CardId == id);
        }

        public void UpdateCard(DiscountCard card, double sum)
        {
            if (card != null)
            {
                dCardsList[dCardsList.IndexOf(card)].TotalSum += sum;
            }
        }
    }
}

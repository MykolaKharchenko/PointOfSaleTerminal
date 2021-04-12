using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            var card = dCardsList.FirstOrDefault(card => card.CardId == id);
            if (card == null)
            {
                card = dCardsList[0];
            }
            return card;
        }

        public void SaveCard(DiscountCard card)
        {
        }
    }
}

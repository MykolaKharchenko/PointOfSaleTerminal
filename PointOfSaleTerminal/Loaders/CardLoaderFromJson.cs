using Newtonsoft.Json;
using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PointOfSaleTerminal.Loaders
{
    class CardLoaderFromJson : ICardLoader
    {
        private List<DiscountCard> cardsList = new List<DiscountCard>();
        public string pathToFile = $"{Environment.ExpandEnvironmentVariables("%UserProfile%")}\\Cards.json";

        public DiscountCard CreateCard()            // Create
        {
            return new DiscountCard() { TotalSum = 0, CardId = "234324" };  // must use Guid
        }

        public DiscountCard GetCard(string id)        //   Read
        {
            if (id == "")
            {
                return CreateCard();
            }
            else
            {
                var cardsJson = File.ReadAllText(pathToFile);
                cardsList = JsonConvert.DeserializeObject<List<DiscountCard>>(cardsJson);

                return cardsList.FirstOrDefault(card => card.CardId == id);
            }
        }

        public void SaveCard(DiscountCard card)        //  Udpate
        {
            cardsList[cardsList.FindIndex(i => i.CardId == card.CardId)] = card;
            SaveChanges();
        }

        public void RemoveCard(string id)        // Delete
        {
            cardsList.Remove(cardsList.FirstOrDefault(card => card.CardId == id));
            SaveChanges();
        }

        private void SaveChanges()
        {
            //File.WriteAllText(pathToFile, JsonConvert.SerializeObject(cardsList));

            using (StreamWriter file = File.CreateText(pathToFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, cardsList);
            }
        }
    }
}

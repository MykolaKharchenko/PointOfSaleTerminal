using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Loaders;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("UnitTestsPoFTerminal")]

namespace PointOfSaleTerminal
{
    public class Terminal : ITerminal
    {
        ICardLoader cardsLoader;
        DiscountCard dCard;
        Products products;
        internal Dictionary<string, int> order = new Dictionary<string, int>();

        public void SetPricing(string source)
        {
            products = new Products(source);
        }

        public void ScanDiscountCard(string cardId)
        {
            cardsLoader = new MockCardsLoader();
            //cardsLoader = new CardLoaderFromJson();
            dCard = cardsLoader.GetCard(cardId);
        }

        public void Scan(string item)
        {
            if (products.GetProduct(item, out _))
            {
                if (order.ContainsKey(item))
                {
                    order[item] += 1;
                }
                else
                {
                    order.Add(item, 1);
                }
            }
            else
            {
                throw new Exception($"product with code {item} was not found in Products");
            }
        }

        public double CalculateTotal()
        {
            double costingPurchase = 0;
            int discountPercent = new DiscountRanges().GetDiscount(dCard);

            foreach (var item in order)
            {
                if (products.GetProduct(item.Key, out var product))
                {
                    costingPurchase += VolumeDiscount(product, item.Value, out var remainder);
                    costingPurchase += CardDiscount(product, item.Value, discountPercent, remainder);
                }
            }
            dCard.TotalSum += costingPurchase;
            cardsLoader.Save(dCard);

            return costingPurchase;
        }

        private double VolumeDiscount(Product product, int volume, out int remainder)
        {
            double total = 0;
            remainder = 0;

            if (product.DiscountCount > 0 && volume >= product.DiscountCount)
            {
                var volumeGroup = Math.DivRem(volume, product.DiscountCount, out remainder);
                total = volumeGroup* product.DiscountPrice;
            }
            return total;
        }

        private double CardDiscount(Product product, int volume, int dPercent, int remainder)
        {
            double total = 0;

            if (remainder > 0)
            {
                volume = remainder;
            }
            total += volume * product.Price;

            //  total += remainder>0 ? remainder * product.Price: volume * product.Price;  -- variant №2

            total = Math.Round(total * (1 - (double)dPercent / 100), 2, MidpointRounding.AwayFromZero);
            return total;
        }
    }
}

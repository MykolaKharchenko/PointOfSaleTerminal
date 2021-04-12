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
        private ICardLoader cardsLoader;
        private Products products;
        internal DiscountCard dCard;
        internal Dictionary<string, int> order = new Dictionary<string, int>();

        public void SetPricing(string source)
        {
            products = new Products(source);
        }

        public void ScanDiscountCard(string cardId)
        {
            //cardsLoader = new MockCardsLoader();
            cardsLoader = new CardLoaderFromJson();
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
            double total = 0;
            int discountPercent = new DiscountRanges().GetDiscount(dCard);

            foreach (var item in order)
            {
                if (products.GetProduct(item.Key, out var product))
                {
                    total += VolumeDiscount(product, item.Value, discountPercent);
                }
            }
            Math.Round(dCard.TotalSum += total,2);
            cardsLoader.SaveCard(dCard);

            return Math.Round(total, 2);
        }

        private double VolumeDiscount(Product product, int volume, int dPercent)
        {
            double total = 0;

            if (product.DiscountCount > 0 && volume >= product.DiscountCount)
            {
                var volumeGroup = Math.DivRem(volume, product.DiscountCount, out var remainder);
                total += volumeGroup * product.DiscountPrice;
                volume = remainder;
            }
            total += CardDiscount(volume, product.Price, dPercent);

            return total;
        }

        private double CardDiscount(int volume, double price, int dPercent)
        {
            double total = 0;

            total += volume * price;
            total = total * (1 - (double)dPercent / 100);
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            return total;
        }
    }
}

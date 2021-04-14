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
            products = new Products(null);
        }

        public void ScanDiscountCard(string cardId)
        {
            cardsLoader = new MockCardsLoader();
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
            cardsLoader.UpdateCard(dCard, Math.Round(total, 2));

            return Math.Round(total, 2);
        }

        private double VolumeDiscount(Product product, int volume, int dPercent)
        {
            double total = 0;
            int volumeToCardDiscount = volume;

            if (product.DiscountCount > 0 && volume >= product.DiscountCount)
            {
                var volumeGroup = Math.DivRem(volume, product.DiscountCount, out var remainder);
                total += volumeGroup * product.DiscountPrice;
                volumeToCardDiscount = remainder;
            }
            total += CardDiscount(volumeToCardDiscount, product.Price, dPercent);

            return total;
        }

        private double CardDiscount(int volume, double price, int dPercent)
        {
            double total = 0;

            total += volume * price;
            total *= (1 - (double)dPercent / 100);
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);

            return total;
        }
    }
}

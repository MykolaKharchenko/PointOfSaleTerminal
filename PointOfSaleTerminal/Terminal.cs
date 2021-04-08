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
        private double totalSumDiscountCard;
        private double sumPurchase = 0;
        ICardLoader cardsLoader;

        internal Dictionary<string, int> order = new Dictionary<string, int>();
        Products products;

        public void SetPricing(string source)
        {
            products = new Products(source);
        }

        public void ScanDiscountCard(string cardId)
        {
            cardsLoader = new MockCardsLoader();
            //cardsLoader = new CardLoaderFromJson();
            totalSumDiscountCard = cardsLoader.GetCard(cardId);
        }

        public void Scan(string item)           // public double Scan(string item)  - return in CalculateTotal
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
            int discountPercent = new DiscountRanges().GetDiscount(totalSumDiscountCard);
            double costingUsingDiscount = 0;

            double costingPurchase = 0;
            foreach (var item in order)
            {
                if (products.GetProduct(item.Key, out Product product))
                {
                    if (product.DiscountCount > 0 && item.Value >= product.DiscountCount)
                    {
                        var quotient = Math.DivRem(item.Value, product.DiscountCount, out var remainder);

                        costingUsingDiscount += remainder * product.Price;
                        costingPurchase += quotient * product.DiscountCount + remainder * product.Price;

                        //result += (item.Value / product.DiscountCount)* product.DiscountPrice;
                        //result += (item.Value % product.DiscountCount) * product.Price;
                    }
                    else
                    {
                        costingUsingDiscount += item.Value * product.Price;
                        costingPurchase += item.Value * product.Price;
                    }
                }
            }
            totalSumDiscountCard += costingUsingDiscount;           // Math.Round(result * (1 - (double)discountPercent / 100), 2);
            //cardsLoader.Save();

            return Math.Round(costingPurchase * (1 - (double)discountPercent / 100), 2);
        }   
    }
}

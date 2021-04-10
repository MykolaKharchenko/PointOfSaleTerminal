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
                    costingPurchase += ByVolumeDiscount(product, item.Value);
                    costingPurchase += ByCardDiscount(product, item.Value, discountPercent);
                }
            }

            dCard.TotalSum += costingPurchase;  // а шо если челик без ДискКарты?
            cardsLoader.Save(dCard);

            return costingPurchase;
        }

        private double ByVolumeDiscount(Product product, int productVolume)
        {
            double productCosting = 0;

            if (product.DiscountCount > 0 && productVolume >= product.DiscountCount)
            {
                productCosting = (productVolume / product.DiscountCount) * product.DiscountPrice;
            }
            return productCosting;
        }

        private double ByCardDiscount(Product product, int productVolume, int dPercent)
        {
            double productCosting = 0;

            if (product.DiscountCount > 0)
            {
                productCosting += (productVolume % product.DiscountCount) * product.Price;
            }
            else
            {
                productCosting += productVolume * product.Price;
            }
            return productCosting = Math.Round(productCosting * (1 - (double)dPercent / 100), 2, MidpointRounding.AwayFromZero);
          //  return productCosting;
        }
    }
}

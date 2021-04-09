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
                    costingPurchase += ProductCostingByVolumeDiscount(product, item.Value);
                    costingPurchase += ProductCostingByCardDiscount(product, item.Value, discountPercent);
                }
            }

            dCard.TotalSum += costingPurchase;  // а шо если челик без ДискКарты?
            cardsLoader.Save(dCard);

            return costingPurchase;
        }

        private double ProductCostingByVolumeDiscount(Product _product, int _productVolume)
        {
            double productCosting = 0;

            if (_product.DiscountCount > 0 && _productVolume >= _product.DiscountCount)
            {
                productCosting = (_productVolume / _product.DiscountCount) * _product.DiscountPrice;
            }
            return productCosting;
        }

        private double ProductCostingByCardDiscount(Product _product, int _productVolume, int _dPercent)
        {
            double productCostingWithoutDiscount = 0;

            if (_product.DiscountCount > 0)
            {
                productCostingWithoutDiscount += (_productVolume % _product.DiscountCount) * _product.Price;
            }
            else
            {
                productCostingWithoutDiscount += _productVolume * _product.Price;
            }

            var productCosting = Math.Round(productCostingWithoutDiscount * (1 - (double)_dPercent / 100), 2, MidpointRounding.AwayFromZero);
            return productCosting;
        }
    }
}

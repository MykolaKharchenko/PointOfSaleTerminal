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
            int discountPercent = new DiscountRanges().GetDiscount(dCard);

            double costingByCardDiscount = 0;
            double costingByVolumeDiscount = 0;

            foreach (var item in order)
            {
                if (products.GetProduct(item.Key, out var product))
                {
                    costingByVolumeDiscount += CostingByVolumeDiscount(product, item.Value);
                    costingByCardDiscount += CostingByCardDiscount(product, item.Value, discountPercent);
                }
            }
            var costingPurchase = costingByVolumeDiscount + costingByCardDiscount;

            dCard.TotalSum += costingPurchase;  // а шо если челик без ДискКарты?
            cardsLoader.Save(dCard);

            return costingPurchase;
        }

        private double CostingByVolumeDiscount(Product _product, int _itemVolume)
        {
            double _costingByVolumeDiscount = 0;

            if (_product.DiscountCount > 0 && _itemVolume >= _product.DiscountCount)
            {
                _costingByVolumeDiscount = (_itemVolume / _product.DiscountCount) * _product.DiscountPrice;
            }

            return _costingByVolumeDiscount;
        }

        private double CostingByCardDiscount(Product _product, int _itemVolume, int _dPercent)
        {
            double _costingForCardDiscount = 0;

            if (_product.DiscountCount > 0 && (_itemVolume % _product.DiscountCount != 0))
            {
                var productVolumeForDiscount = Math.DivRem(_itemVolume, _product.DiscountCount, out var productVolumeWithoutDiscount);

                _costingForCardDiscount += productVolumeWithoutDiscount * _product.Price;
            }
            else
            {
                _costingForCardDiscount += _itemVolume * _product.Price;
            }

            var costingByCardDiscount = Math.Round(_costingForCardDiscount * (1 - (double)_dPercent / 100), 2, MidpointRounding.AwayFromZero);

            return costingByCardDiscount;
        }

        //private double CostingByVolumeDiscount2(Dictionary<string, int> _order)
        //{
        //    double costingByVolumeDiscount = 0;

        //    foreach (var item in _order)
        //    {
        //        if (products.GetProduct(item.Key, out var product))
        //        {
        //            if (product.DiscountCount > 0 && item.Value >= product.DiscountCount)
        //            {
        //                costingByVolumeDiscount = (item.Value / product.DiscountCount) * product.DiscountPrice;
        //            }
        //        }
        //    }
        //    return costingByVolumeDiscount;
        //}

        //private double CostingByCardDiscount2(Dictionary<string, Product> _order, int _dPercent)
        //{
        //    double costingForCardDiscount = 0;

        //    foreach (var item in order)
        //    {
        //        if (products.GetProduct(item.Key, out var product))
        //        {
        //            if (product.DiscountCount > 0 && (item.Value % product.DiscountCount !=0))
        //            {
        //                var productVolumeForDiscount = Math.DivRem(item.Value, product.DiscountCount, out var productVolumeWithoutDiscount);

        //                costingForCardDiscount += productVolumeWithoutDiscount * product.Price;
        //            }
        //            else
        //            {
        //                costingForCardDiscount += item.Value * product.Price;
        //            }
        //        }
        //    }
        //    var costingByCardDiscount = Math.Round(costingForCardDiscount * (1 - (double)_dPercent / 100), 2, MidpointRounding.AwayFromZero);

        //    return 0;
        //}
    }
}

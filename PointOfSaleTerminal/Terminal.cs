using System;
using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Loaders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("UnitTestsPoFTerminal")]

namespace PointOfSaleTerminal
{
    public class Terminal : ITerminal
    {
        private DiscountCard dCard;

        internal Dictionary<string, int> order = new Dictionary<string, int>();
        private Dictionary<string, Product> products = new Dictionary<string, Product>();

        public void SetPricing(string source)
        {
            var productsLoader = new ProductLoaderFromJson();
            products = productsLoader.LoadProducts(source);
        }

        public void VerifyDiscountCard(double _)
        {
            dCard = new DiscountCard(_);
            //dCard = new DiscountCard() { 546, 3 };
        }

        public void Scan(string item)
        {
            if (products.ContainsKey(item))
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
            double result = 0;
            double resultWithoutDiscounts = 0;
            foreach (var item in order)
            {
                if (products.TryGetValue(item.Key, out var product))
                {
                    if (product.DiscountCount > 0 && item.Value >= product.DiscountCount)
                    {
                        result += product.DiscountPrice;
                        result += (item.Value - product.DiscountCount) * product.Price;
                    }
                    else
                    {
                        resultWithoutDiscounts += product.Price;
                        result += item.Value * product.Price;
                    }
                }
            }
            //dCard.Amount += resultWithoutDiscounts;    // Payment(resultWithoutDiscounts);             // - сума інкрементуюча на дисконтну картку ПІСЛЯ сплати
            return result;// * (1 - dcard.DiscountPercent / 100);                                         // - сумма до сплати
        }

        public void Payment(double amount)
        {
            //var amountToPay = CalculateTotal();
            //    dcard.Amount += amount;
            //~customer 
        }
    }
}

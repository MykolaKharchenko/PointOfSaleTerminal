using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("UnitTestsPoFTerminal")]
namespace PointOfSaleTerminal.Loaders
{
    internal class MockProductsLoader : IProductsLoader
    {
        public Dictionary<string, Product> LoadProducts(string _)
        {
            var products = new Dictionary<string, Product>()
            {
                { "A" ,new Product { Price = 1.25, DiscountCount = 3, DiscountPrice = 3 } },
                { "B" ,new Product { Price = 4.25, } },
                { "C" ,new Product { Price = 1, DiscountCount = 6, DiscountPrice = 5 } },
                { "D" ,new Product { Price = 0.75,} },
            };
            return products;
        }
    }
}

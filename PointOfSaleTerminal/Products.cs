using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Loaders;
using System.Collections.Generic;

namespace PointOfSaleTerminal
{
    public class Products
    {
        private Dictionary<string, Product> products = new Dictionary<string, Product>();

        public Products(string source)
        {
            var productsLoader = new ProductLoaderFromJson();
            products = productsLoader.LoadProducts(source);
        }

        public bool GetProduct(string key, out Product product)
        {
            return products.TryGetValue(key, out product);
        }
    }
}

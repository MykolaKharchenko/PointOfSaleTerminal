using Newtonsoft.Json;
using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace PointOfSaleTerminal.Loaders
{
   internal class ProductLoaderFromJson : IProductsLoader
    {
        public Dictionary<string, Product> LoadProducts(string sourcePath)
        {
            var productsJson = File.ReadAllText(sourcePath);
            return JsonConvert.DeserializeObject<Dictionary<string, Product>>(productsJson);
        }
    }
}

using PointOfSaleTerminal.DataModels;
using System.Collections.Generic;

namespace PointOfSaleTerminal.Interfaces
{
    internal interface IProductsLoader
    {
        Dictionary<string, Product> LoadProducts(string sourceName);
    }
}

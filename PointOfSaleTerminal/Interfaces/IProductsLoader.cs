using PointOfSaleTerminal.DataModels;
using System.Collections.Generic;

namespace PointOfSaleTerminal.Interfaces
{
    public interface IProductsLoader
    {
        Dictionary<string, Product> LoadProducts(string sourceName);
    }
}

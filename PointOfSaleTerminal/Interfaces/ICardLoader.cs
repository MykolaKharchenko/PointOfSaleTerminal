using PointOfSaleTerminal.DataModels;

namespace PointOfSaleTerminal.Interfaces
{
    public interface ICardLoader
    {
        DiscountCard GetCard(string id);
    }
}

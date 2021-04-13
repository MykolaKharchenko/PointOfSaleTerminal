using PointOfSaleTerminal.DataModels;

namespace PointOfSaleTerminal.Interfaces
{
    public interface ICardLoader
    {
        DiscountCard GetCard(string id);
        void UpdateCard(DiscountCard card , double total);
    }
}

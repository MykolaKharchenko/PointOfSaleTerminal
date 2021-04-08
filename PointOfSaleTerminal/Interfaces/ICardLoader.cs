using PointOfSaleTerminal.DataModels;

namespace PointOfSaleTerminal.Interfaces
{
    public interface ICardLoader
    {
        double GetCard(string id);
        void SaveData(double totalSum);
    }
}

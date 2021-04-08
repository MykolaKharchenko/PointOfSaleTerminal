namespace PointOfSaleTerminal.Interfaces
{
    public interface ITerminal
    {
        void Scan(string item);
        void SetPricing(string source);
        double CalculateTotal();
        void ScanDiscountCard(string cardId);
    }
}

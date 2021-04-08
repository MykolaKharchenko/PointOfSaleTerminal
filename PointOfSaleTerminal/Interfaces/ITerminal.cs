namespace PointOfSaleTerminal.Interfaces
{
    internal interface ITerminal
    {
        void Scan(string item);
        void SetPricing(string source);
        double CalculateTotal();
    }
}

using PointOfSaleTerminal;
using PointOfSaleTerminal.Interfaces;
using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ITerminal terminal = new Terminal();
            terminal.SetPricing("Products.json");

            terminal.ScanDiscountCard("0");

            for (int i = 0; i < 30; i++)
                terminal.Scan("A");
            terminal.Scan("B");
            terminal.Scan("C");

            var amountPurchase = terminal.CalculateTotal();
            Console.WriteLine(amountPurchase);
        }
    }
}

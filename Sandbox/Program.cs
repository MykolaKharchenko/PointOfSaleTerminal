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

            terminal.ScanDiscountCard("7");

            for (int i = 0; i < 32; i++)
                terminal.Scan("A");
            for (int i = 0; i < 12; i++)
                terminal.Scan("B");
            for (int i = 0; i < 30; i++)
                terminal.Scan("C");
            for (int i = 0; i < 22; i++)
                terminal.Scan("D");

            var amountPurchase = terminal.CalculateTotal();
            Console.WriteLine(amountPurchase);
        }
    }
}

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
            terminal.SetPricing("string");
            terminal.ScanDiscountCard("1");

            var data = new string[] { "A", "A", "A", "B", "B", "B", "B", "C", "C", "C", "C", "C", "C", "D", "D", "D", "D" };
            for (int i = 0; i < data.Length; i++)
                terminal.Scan(data[i]);

            //for (int i = 0; i < 6; i++)
            //    terminal.Scan("A");
            //for (int i = 0; i < 200; i++)
            //    terminal.Scan("B");
            //for (int i = 0; i < 30; i++)
            //    terminal.Scan("C");
            //for (int i = 0; i < 22; i++)
            //    terminal.Scan("D");

            var amountPurchase = terminal.CalculateTotal();
        }
    }
}

﻿using PointOfSaleTerminal;
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

            terminal.ScanDiscountCard("sdfds");
            //var data = new string[] { "A", "B", "C", "C", "C", "D", "D", "D", "D", "A", "D" };
            //for (int i = 0; i < data.Length ; i++)
            //    terminal.Scan(data[i]);

            //for (int i = 0; i < 32; i++)
            //    terminal.Scan("A");
            for (int i = 0; i < 200; i++)
                terminal.Scan("B");
            //for (int i = 0; i < 30; i++)
            //    terminal.Scan("C");
            //for (int i = 0; i < 22; i++)
            //    terminal.Scan("D");

            var amountPurchase = terminal.CalculateTotal();
            Console.WriteLine(amountPurchase);
        }
    }
}

using PointOfSaleTerminal;
using PointOfSaleTerminal.Interfaces;
using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var costingByCardDiscount00 = Math.Round(5.444, 2);
            var costingByCardDiscount01 = Math.Round(5.445, 2);  // false = 5.44!
            var costingByCardDiscount02 = Math.Round(5.446, 2);

            var costingByCardDiscount0 = Math.Round(5.444, 2, MidpointRounding.AwayFromZero);
            var costingByCardDiscount1 = Math.Round(5.445, 2, MidpointRounding.AwayFromZero);
            var costingByCardDiscount2 = Math.Round(5.446, 2, MidpointRounding.AwayFromZero);
            var costingByCardDiscount3 = Math.Round(5.444, 2, MidpointRounding.ToEven);
            var costingByCardDiscount4 = Math.Round(5.445, 2, MidpointRounding.ToEven);     // false = 5.44!
            var costingByCardDiscount5 = Math.Round(5.446, 2, MidpointRounding.ToEven);

            var costingByCardDiscount6 = Math.Round(200.5, MidpointRounding.ToEven);
            var costingByCardDiscount7 = Math.Round(200.5, MidpointRounding.AwayFromZero);

            var costingByCardDiscount8 = Math.Round(200.5,1, MidpointRounding.ToEven);
            var costingByCardDiscount9 = Math.Round(200.5,1, MidpointRounding.AwayFromZero);


            ITerminal terminal = new Terminal();
            terminal.SetPricing("Products.json");                 // shoudl i realize it by async?

            terminal.ScanDiscountCard("1");                         //  scaning ДисконтКарты by id

            for (int i = 0; i < 31; i++)
                terminal.Scan("A");
            terminal.Scan("B");

            var amountPurchase = terminal.CalculateTotal();       // подсчет СуммыТекущейПокупки
            Console.WriteLine(amountPurchase);                    // вывод  для себя: суммы покупки
        }
    }
}

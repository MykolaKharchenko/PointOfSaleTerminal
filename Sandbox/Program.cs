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
            terminal.SetPricing("Products.json");                 // shoudl i realize it by async?

            terminal.ScanDiscountCard(1);                         //  scaning ДисконтКарты by id

            for (int i = 0; i < 31; i++)
                terminal.Scan("A");
            terminal.Scan("B");

            var amountPurchase = terminal.CalculateTotal();       // подсчет СуммыТекущейПокупки
            Console.WriteLine(amountPurchase);                    // вывод  для себя: суммы покупки
            terminal.Payment(false);                              // типа НЕоплата и инкрементация ДисконтКарты СуммыПокупок
            terminal.Payment(true);                               // типа оплата и инкрементация ДисконтКарты СуммыПокупок
        }
    }
}

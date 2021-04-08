using PointOfSaleTerminal;
using PointOfSaleTerminal.DataModels;
using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //DiscountCard dCard1 = new DiscountCard(0);
            //Console.WriteLine("Amount= " + dCard1.Amount.ToString() + " |Percent= " + dCard1.DiscountPercent);

            //DiscountCard dCard2 = new DiscountCard(1500);
            //Console.WriteLine("Amount= " + dCard2.Amount.ToString() + " |Percent= " + dCard2.DiscountPercent);

            //DiscountCard dCard3 = new DiscountCard(3000);
            //Console.WriteLine("Amount= " + dCard3.Amount.ToString() + " |Percent= " + dCard3.DiscountPercent);

            //DiscountCard dCard4 = new DiscountCard(6000);
            //Console.WriteLine("Amount= " + dCard4.Amount.ToString() + " |Percent= " + dCard4.DiscountPercent);

            //DiscountCard dCard5 = new DiscountCard(60000);
            //Console.WriteLine("Amount= " + dCard5.Amount.ToString() + " |Percent= " + dCard5.DiscountPercent);



            

            var terminal = new Terminal();
            terminal.SetPricing("Products.json");

            terminal.VerifyDiscountCard(65854654654);   //  инициализация ДисконтКарты                  maybe using Console.Readline() for debuging ?
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");

            var amountPurchase = terminal.CalculateTotal();     // подсчет СуммыТекущейПокупкил
            Console.WriteLine(amountPurchase);      // вывод  для себя: суммы покупки
            terminal.Payment(amountPurchase);       // типа оплата и инкрементация ДисконтКарты СуммыПокупок
        }
    }
}

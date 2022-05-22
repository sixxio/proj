using System;
using System.Collections.Generic;
using System.Threading;

namespace ObserverSubscribers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var Publisher = new Publisher();

            var client1 = new Subscriber1(3000, "Эмиль");
            Publisher.Attach(client1);

            var client2 = new Subscriber2(1700, "Кофейный маньяк");
            Publisher.Attach(client2);

            List<string> newProducts = new List<string>();

            newProducts.Add("Бекон из индейки ИНДИЛАЙТ");
            Publisher.AddSomeProducts(newProducts);

            newProducts.Add("Дрип-пакеты VERLE");
            Publisher.AddSomeProducts(newProducts);

            Console.WriteLine("\nКлиент {0} заказал {1} товар(ов).", client1.name, client1.boughtItems.Count);
            Console.WriteLine("Клиент {0} заказал {1} товар(ов).", client2.name, client2.boughtItems.Count);
        }
    }
}

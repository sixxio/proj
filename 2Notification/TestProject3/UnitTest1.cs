using NUnit.Framework;
using Moq;
using ObserverSubscribers;
using System.Collections.Generic;

namespace TestProject3
{
    public class Tests
    {

        [Test]
        public void Test1()
        {

            var Publisher = new Publisher();
            var client1 = new Subscriber1(3000, "Никита");
            Publisher.Attach(client1);

            var client2 = new Subscriber2(1700, "Андрей");
            Publisher.Attach(client2);

            List<string> newProducts = new List<string>();

            newProducts.Add("Кофе Bushido");

            Publisher.AddSomeProducts(newProducts);

            Assert.IsTrue(client2.boughtItems.Contains("Кофе Bushido"));

            newProducts.Add("Сырок Б.Ю. Александров");
            Publisher.AddSomeProducts(newProducts);

            Assert.IsTrue(client1.boughtItems.Contains("Сырок Б.Ю. Александров"));
        }
    }
}
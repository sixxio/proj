using NUnit.Framework;
using ObserverSubscribers;
using System.Collections.Generic;

namespace TestProject1
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


            mock.Setup(p => p.Send(It.IsAny<string>())).Verifiable();

        }
    }
}
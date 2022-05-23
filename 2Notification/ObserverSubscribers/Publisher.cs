using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    
    public class Publisher : IPublisher
    {

        private List<string> products = new List<string>();
        private List<ISubscriber> subscribers = new List<ISubscriber>();
        public void Attach(ISubscriber observer)
        {
            Console.WriteLine($"Самокат: {(observer as Subscriber).name} подписался на нас :)");
            this.subscribers.Add(observer);
        }
        public void Detach(ISubscriber observer)
        {
            this.subscribers.Remove(observer);
            Console.WriteLine($"Самокат: теперь {(observer as Subscriber).name} отписался от нас :(");
        }
        public void Notify()
        {
            Console.WriteLine("\nСамокат: уведомление подписчиков..");

            foreach (var observer in subscribers)
            {
                Thread.Sleep((observer as Subscriber).timeToNoify);
                observer.Update(this);
            }
        }
        public void AddSomeProducts(List<string> products)
        {
            this.products.AddRange(products);

            Console.WriteLine($"\nСамокат: нам поступили новые товары:");

            foreach (var product in products)
                Console.WriteLine($"В дарксторе появился товар - {product}");

            Notify();
        }
        public List<string> GetProductsList()
        {
            return this.products;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    public class Subscriber1 : Subscriber, ISubscriber
    {
        public Subscriber1(int subTime, string subName) : base(subTime, subName)
        {
            this.timeToNoify = subTime;
            this.name = subName;
        }
        public void Update(IPublisher publisher)
        {
            foreach (var item in (publisher as Publisher).GetProductsList())
            {
                if (item == "Бекон из индейки ИНДИЛАЙТ" && !boughtItems.Contains("Бекон из индейки ИНДИЛАЙТ"))
                {
                    Console.WriteLine("Subscriber1: Юху, как раз делаю сэндвичи!");
                    boughtItems.Add(item);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    public class Subscriber2 : Subscriber, ISubscriber
    {
        public Subscriber2(int subTime, string subName) : base(subTime, subName)
        {
            this.timeToNoify = subTime;
            this.name = subName;
        }
        public void Update(IPublisher publisher)
        {
            foreach (var item in (publisher as Publisher).GetProductsList())
            {
                if (item == "Дрип-пакеты VERLE" && !boughtItems.Contains("Дрип-пакеты VERLE"))
                {
                    Console.WriteLine("Subscriber2: Супер, я как раз хотел заварить дрип!");
                    boughtItems.Add(item);
                }
            }
        }
    }
}

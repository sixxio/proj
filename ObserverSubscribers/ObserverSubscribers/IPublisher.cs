using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    public interface IPublisher
    {
        void Attach(ISubscriber observer);
        void Detach(ISubscriber observer);
        void AddSomeProducts(List<string> products) { this.Notify(); }
        public void Notify();
    }
}

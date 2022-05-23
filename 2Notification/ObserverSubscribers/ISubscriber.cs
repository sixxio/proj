using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    public interface ISubscriber
    {
        void Update(IPublisher subject);
    }
}

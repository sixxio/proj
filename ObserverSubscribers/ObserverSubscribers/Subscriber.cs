using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscribers
{
    public class Subscriber
    {
        public int timeToNoify = 0;
        public string name = "new subscriber";
        public List<string> boughtItems = new List<string>();
        public Subscriber(int subTime, string subName)
        {
            this.timeToNoify = subTime;
            this.name = subName;
        }
    }
}

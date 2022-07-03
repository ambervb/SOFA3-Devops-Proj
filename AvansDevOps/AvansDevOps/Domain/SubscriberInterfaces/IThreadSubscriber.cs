using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SubscriberInterfaces
{
    public interface IThreadSubscriber
    {
        public void ThreadUpdate(Thread thread);
    }
}

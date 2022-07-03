using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.NotificationAdapter
{
    public class SlackAdapter : INotificationAdapter
    {
        public void SendNotification(string data)
        {
            Console.WriteLine("SLACK_ADAPTER: New message in thread " + data);
        }
    }
}

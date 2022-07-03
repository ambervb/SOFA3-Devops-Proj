using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.NotificationAdapter
{
    public class MailAdapter : INotificationAdapter
    {
        public void SendNotification(string data)
        {
            Console.WriteLine("MAIL_ADAPTER: New message in thread " + data);
        }
    }
}

using AvansDevOps.Domain.SubscriberInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public class Thread
    {
        public string _title { get; set; }
        public List<Reaction> _reactions { get; set; }
        public List<IThreadSubscriber> _subscribers { get; set; }
        public bool _isLocked { get; set; }

        public Thread(string title, User user)
        {
            _title = title;
            _reactions = new List<Reaction>();
            _subscribers = new List<IThreadSubscriber> { user };
            _isLocked = false;
        }

        public void AddReaction(Reaction reaction, User user)
        {
            if (_isLocked)
            {
                Console.WriteLine("THREAD_ITEM_ERROR: Can't add reaction to locked thread");
            }
            else
            {
                _reactions.Add(reaction);
                if (!_subscribers.Contains(user))
                {
                    _subscribers.Add(user);
                }
                NotifySubscriber();
            }
        }

        public void Subscribe(IThreadSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IThreadSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void NotifySubscriber()
        {
            foreach (IThreadSubscriber subscriber in _subscribers)
            {
                subscriber.ThreadUpdate(this);
            }
        }
    }
}

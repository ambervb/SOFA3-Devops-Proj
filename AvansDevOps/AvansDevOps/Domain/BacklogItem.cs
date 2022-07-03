using AvansDevOps.Domain.BacklogItemStates;
using AvansDevOps.Domain.ActivityStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Domain.SubscriberInterfaces;

namespace AvansDevOps.Domain
{
    public class BacklogItem
    {
        public string _requirement { get; set; }
        public User _user { get; set; }
        public Sprint _sprint { get; set; }
        public List<Activity> _activities { get; set; }
        public BacklogItemState _backlogItemState { get; set; }
        public List<Thread> _threads { get; set; }
        public List<IBacklogItemSubscriber> _subscribers { get; set; }

        public BacklogItem(string requirement)
        {
            _requirement = requirement;
            _activities = new List<Activity>();
            _backlogItemState = new TodoBacklogItemState();
            _threads = new List<Thread>();
            _subscribers = new List<IBacklogItemSubscriber>();
        }

        public void AddActivity(Activity activity)
        {
            _activities.Add(activity);
        }

        public void AddThread(Thread thread)
        {
            _threads.Add(thread);
        }

        public void ChangeBacklogItem(string requirementChange)
        {
            if (_sprint._isLocked)
            {
                Console.WriteLine("Can't change backlogitem when sprint is locked");
            }
            else
            {
                _requirement = requirementChange;
            }
        }

        public void CloseThreads()
        {
            foreach (Thread thread in _threads)
            {
                thread._isLocked = true;
            }
        }

        public void Subscribe(IBacklogItemSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IBacklogItemSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void NotifySubscriber(string message)
        {
            foreach (IBacklogItemSubscriber subscriber in _subscribers)
            {
                subscriber.BacklogItemUpdate(message);
            }
        }

        // Backlog item states
        public void ToTodo()
        {
            _backlogItemState.ToTodo(this);
        }

        public void ToDoing()
        {
            _backlogItemState.ToDoing(this);
        }

        public void ToRft()
        {
            _backlogItemState.ToRft(this);
            NotifySubscriber("Backlogitem moved to Ready For Testing");
        }

        public void ToRft(User user)
        {
            _backlogItemState.ToRft(this, user);
        }

        public void ToTesting(User user)
        {
            _backlogItemState.ToTesting(this, user);
        }

        public void ToTested()
        {
            _backlogItemState.ToTested(this);
        }

        public void ToDone(User user)
        {
            foreach (Activity activity in _activities)
            {
                if (!ReferenceEquals(activity._activityState.GetType(), typeof(DoneActivityState)))
                {
                    Console.WriteLine("BACKLOG_ITEM_ERROR: Not all activities are DONE");
                    return;
                }
            }
            _backlogItemState.ToDone(this, user);
        }
    }
}

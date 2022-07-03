using AvansDevOps.Domain.NotificationAdapter;
using AvansDevOps.Domain.SubscriberInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public abstract class User : IThreadSubscriber, IPipelineSubscriber, ISprintSubscriber, IBacklogItemSubscriber
    {
        public string _name { get; set; }
        public List<INotificationAdapter> _notificationAdapters { get; set; }

        protected User(string name)
        {
            _name = name;
            _notificationAdapters = new List<INotificationAdapter>();
        }

        public void ThreadUpdate(Thread thread)
        {
            foreach (INotificationAdapter notificationAdapter in _notificationAdapters)
            {
                notificationAdapter.SendNotification(thread._title);
            }
        }

        public void PipelineUpdate(string pipelineMessage)
        {
            foreach (INotificationAdapter notificationAdapter in _notificationAdapters)
            {
                notificationAdapter.SendNotification(pipelineMessage);
            }
        }

        public void SprintUpdate(string sprintMessage)
        {
            foreach (INotificationAdapter notificationAdapter in _notificationAdapters)
            {
                notificationAdapter.SendNotification(sprintMessage);
            }
        }

        public void BacklogItemUpdate(string backlogItemMessage)
        {
            foreach (INotificationAdapter notificationAdapter in _notificationAdapters)
            {
                notificationAdapter.SendNotification(backlogItemMessage);
            }
        }

        public void AddNotificationAdapter(INotificationAdapter notificationAdapter)
        {
            _notificationAdapters.Add(notificationAdapter);
        }

        public void RemoveNotificationAdapter(INotificationAdapter notificationAdapter)
        {
            _notificationAdapters.Remove(notificationAdapter);
        }

        // Role functions
        public virtual bool CanManagePipeline()
        {
            Console.WriteLine("ERROR: You can not manage the pipeline");
            return false;
        }

        public virtual bool CanMoveBacklogItemFromTested()
        {
            Console.WriteLine("ERROR: You can not move the backlog item to done");
            return false;
        }

        public virtual bool CanUploadReview()
        {
            Console.WriteLine("ERROR: You can not upload the review");
            return false;
        }

        public virtual bool CanCancelSprint()
        {
            Console.WriteLine("ERROR: You can not cancel the sprint");
            return false;
        }
    }
}

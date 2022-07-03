using AvansDevOps.Domain.ActivityStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public class Activity
    {
        public string _task { get; set; }
        public User _user { get; set; }
        public ActivityState _activityState { get; set; }

        public Activity(string task)
        {
            _task = task;
            _activityState = new CreatedActivityState();
        }

        public virtual void ToInProgress()
        {
            _activityState.ToInProgress(this);
        }

        public virtual void ToDone()
        {
            _activityState.ToDone(this);
        }
    }
}

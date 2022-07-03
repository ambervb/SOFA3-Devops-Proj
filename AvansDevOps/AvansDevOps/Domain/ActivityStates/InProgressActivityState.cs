using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.ActivityStates
{
    public class InProgressActivityState : ActivityState
    {
        public override void ToDone(Activity ctx)
        {
            ctx._activityState = new DoneActivityState();
        }
    }
}

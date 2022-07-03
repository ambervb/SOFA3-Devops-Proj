using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.ActivityStates
{
    public class CreatedActivityState : ActivityState
    {
        public override void ToInProgress(Activity ctx)
        {
            ctx._activityState = new InProgressActivityState();
        }
    }
}

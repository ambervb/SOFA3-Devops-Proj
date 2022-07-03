using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.ActivityStates
{
    public abstract class ActivityState
    {
        public virtual void ToInProgress(Activity ctx)
        {
            WriteErrorMessage();
        }

        public virtual void ToDone(Activity ctx)
        {
            WriteErrorMessage();
        }

        private void WriteErrorMessage()
        {
            Console.WriteLine("WRONG_ACTIVITY_STATE: Unable to go to this state");
        }
    }
}

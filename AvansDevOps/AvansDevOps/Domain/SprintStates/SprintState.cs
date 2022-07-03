using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SprintStates
{
    public abstract class SprintState
    {
        public virtual void FinishSprint(Sprint ctx)
        {
            WriteErrorMessage();
        }

        public virtual void StartPipeline(Sprint ctx)
        {
            WriteErrorMessage();
        }

        public virtual void StartReview(Sprint ctx)
        {
            WriteErrorMessage();
        }

        public virtual void CloseSprint(Sprint ctx)
        {
            WriteErrorMessage();
        }

        public virtual void CloseSprint(Sprint ctx, User user)
        {
            WriteErrorMessage();
        }

        private void WriteErrorMessage()
        {
            Console.WriteLine("WRONG_SPRINT_STATE: Unable to go to this state");
        }
    }
}

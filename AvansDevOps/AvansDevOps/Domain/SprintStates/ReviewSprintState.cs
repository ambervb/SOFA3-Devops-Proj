using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SprintStates
{
    public class ReviewSprintState : SprintState
    {
        public override void CloseSprint(Sprint ctx)
        {
            if (ctx._hasReview)
            {
                ctx._sprintState = new ClosedSprintState();
                Console.WriteLine("NEXT_STATE: From reviewSprint to closedSprint state");
            }
        }
    }
}

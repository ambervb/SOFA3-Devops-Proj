using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SprintStates
{
    public class PipelineSprintState : SprintState
    {
        public override void CloseSprint(Sprint ctx)
        {
            ctx._sprintState = new ClosedSprintState();
            Console.WriteLine("NEXT_STATE: From pipelineSprint to closedSprint state");
        }

        public override void StartReview(Sprint ctx)
        {
            ctx._sprintState = new ReviewSprintState();
            Console.WriteLine("NEXT_STATE: From pipelineSprint to reviewSprint state");
        }

        public override void FinishSprint(Sprint ctx)
        {
            ctx._sprintState = new FinishedSprintState();
            Console.WriteLine("NEXT_STATE: From pipelineSprint to finishedSprint state");
        }
    }
}

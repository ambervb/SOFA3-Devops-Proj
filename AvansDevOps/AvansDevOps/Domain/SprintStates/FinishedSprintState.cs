using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SprintStates
{
    public class FinishedSprintState : SprintState
    {
        public override void StartPipeline(Sprint ctx)
        {
            ctx.RunPipeline();
            ctx._sprintState = new PipelineSprintState();
            Console.WriteLine("NEXT_STATE: From finishedSprint to pipelineSprint state");
        }

        public override void StartReview(Sprint ctx)
        {
            ctx._sprintState = new ReviewSprintState();
            Console.WriteLine("NEXT_STATE: From finishedSprint to reviewSprint state");
        }

        public override void CloseSprint(Sprint ctx)
        {
            ctx._sprintState = new ClosedSprintState();
            Console.WriteLine("NEXT_STATE: From finishedSprint to closedSprint state");
        }

        public override void CloseSprint(Sprint ctx, User user)
        {
            if (user.CanCancelSprint())
            {
                ctx._sprintState = new ClosedSprintState();
                Console.WriteLine("NEXT_STATE: From finishedSprint to closedSprint state");
            }
        }
    }
}

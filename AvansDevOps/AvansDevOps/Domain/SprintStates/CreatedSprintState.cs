using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.SprintStates
{
    public class CreatedSprintState : SprintState
    {
        public override void FinishSprint(Sprint ctx)
        {
            ctx._isLocked = true;
            ctx._sprintState = new FinishedSprintState();
            Console.WriteLine("NEXT_STATE: From createdSprint to finishedSprint state");
        }
    }
}

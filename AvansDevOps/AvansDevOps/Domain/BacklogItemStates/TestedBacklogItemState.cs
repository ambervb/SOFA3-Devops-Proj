using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public class TestedBacklogItemState : BacklogItemState
    {
        public override void ToDone(BacklogItem ctx, User user)
        {
            if (IsAllowed(user))
            {
                ctx.CloseThreads();
                ctx._backlogItemState = new DoneBacklogItemState();
                Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From tested to done state");
            }
        }

        public override void ToRft(BacklogItem ctx, User user)
        {
            if (IsAllowed(user))
            {
                ctx._backlogItemState = new ReadyForTestingBacklogItemState();
                Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From tested to readyForTesting state");
            }
        }

        private bool IsAllowed(User user)
        {
            return user.CanMoveBacklogItemFromTested();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public class TodoBacklogItemState : BacklogItemState
    {
        public override void ToDoing(BacklogItem ctx)
        {
            ctx._backlogItemState = new DoingBacklogItemState();
            Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From todo to doing state");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public class DoingBacklogItemState : BacklogItemState
    {
        public override void ToRft(BacklogItem ctx)
        {
            ctx._backlogItemState = new ReadyForTestingBacklogItemState();
            Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From doing to readyForTesting state");
        }
    }
}

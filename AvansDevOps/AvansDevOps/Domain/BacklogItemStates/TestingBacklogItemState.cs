using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public class TestingBacklogItemState : BacklogItemState
    {
        public override void ToTested(BacklogItem ctx)
        {
            ctx._backlogItemState = new TestedBacklogItemState();
            Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From testing to tested state");
        }

        public override void ToTodo(BacklogItem ctx)
        {
            ctx._backlogItemState = new TodoBacklogItemState();
            ctx.NotifySubscriber("Backlogitem moved to TODO");
            Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From testing to todo state");
        }
    }
}

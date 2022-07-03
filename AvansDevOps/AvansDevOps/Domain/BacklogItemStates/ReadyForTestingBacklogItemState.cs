using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public class ReadyForTestingBacklogItemState : BacklogItemState
    {
        public override void ToTesting(BacklogItem ctx, User user)
        {
            if (user != ctx._user)
            {
                ctx._backlogItemState = new TestingBacklogItemState();
                Console.WriteLine("NEXT_BACKLOG_ITEM_STATE: From readyForTesting to testing state");
            }
            else
            {
                Console.WriteLine("BACKLOG_ITEM: User cannot move their own item to testing");
            }
        }
    }
}

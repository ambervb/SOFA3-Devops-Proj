using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.BacklogItemStates
{
    public abstract class BacklogItemState
    {
        public virtual void ToTodo(BacklogItem ctx)
        {
            WriteErrorMessage();
        }

        public virtual void ToDoing(BacklogItem ctx)
        {
            WriteErrorMessage();
        }

        public virtual void ToRft(BacklogItem ctx)
        {
            WriteErrorMessage();
        }

        public virtual void ToRft(BacklogItem ctx, User user)
        {
            WriteErrorMessage();
        }

        public virtual void ToTesting(BacklogItem ctx, User user)
        {
            WriteErrorMessage();
        }

        public virtual void ToTested(BacklogItem ctx)
        {
            WriteErrorMessage();
        }

        public virtual void ToDone(BacklogItem ctx, User user)
        {
            WriteErrorMessage();
        }

        private void WriteErrorMessage()
        {
            Console.WriteLine("WRONG_BACKLOG_ITEM_STATE: Unable to go to this backlog state");
        }
    }
}

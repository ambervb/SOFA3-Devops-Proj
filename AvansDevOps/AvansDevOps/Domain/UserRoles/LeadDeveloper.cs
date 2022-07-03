using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.UserRoles
{
    public class LeadDeveloper : User
    {
        public LeadDeveloper(string name) : base(name)
        {
        }

        public override bool CanMoveBacklogItemFromTested()
        {
            Console.WriteLine("ACTION: User is allowed to move backlog item to DONE");
            return true;
        }
    }
}

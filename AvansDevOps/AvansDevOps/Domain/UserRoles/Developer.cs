using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.UserRoles
{
    public class Developer : User
    {
        public Developer(string name) : base(name)
        {
        }
    }
}

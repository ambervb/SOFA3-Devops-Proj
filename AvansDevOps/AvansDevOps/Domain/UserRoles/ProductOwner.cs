using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.UserRoles
{
    public class ProductOwner : User
    {
        public ProductOwner(string name) : base(name)
        {
        }
    }
}

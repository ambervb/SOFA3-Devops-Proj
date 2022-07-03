using AvansDevOps.Domain.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Factories
{
    public class UserFactory
    {
        //User presses button and correct method is called
        public User? CreateUser(string name, string role)
        {
            if (role.Equals("developer"))
            {
                return new Developer(name);
            } 
            else if (role.Equals("lead-developer"))
            {
                return new LeadDeveloper(name);
            }
            else if (role.Equals("scrum-master"))
            {
                return new ScrumMaster(name);
            }
            else if (role.Equals("product-owner"))
            {
                return new ProductOwner(name);
            }
            else
            {
                Console.WriteLine("Role doesn't exist");
            }
            return null;
        }
    }
}

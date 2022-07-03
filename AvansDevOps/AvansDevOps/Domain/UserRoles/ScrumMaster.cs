using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.UserRoles
{
    public class ScrumMaster : User
    {
        public ScrumMaster(string name) : base(name)
        {
        }

        public override bool CanManagePipeline()
        {
            Console.WriteLine("ACTION: User is allowed to manage Pipeline");
            return true;
        }

        public override bool CanUploadReview()
        {
            Console.WriteLine("ACTION: User is allowed to upload a review document");
            return true;
        }
        public override bool CanCancelSprint()
        {
            Console.WriteLine("ACTION: User is allowed to cancel a sprint");
            return true;
        }
    }
}

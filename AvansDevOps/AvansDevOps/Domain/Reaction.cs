using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public class Reaction
    {
        public string _reactionContent { get; set; }

        public Reaction(string reactionContent)
        {
            _reactionContent = reactionContent;
        }
    }
}

using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite
{
    public abstract class Component
    {
        //operation methods
        public string _name { get; set; }

        public Component(string name)
        {
            _name = name;
        }

        public abstract void AcceptVisitor(PipelineVisitor pipelineVisitor);
    }
}

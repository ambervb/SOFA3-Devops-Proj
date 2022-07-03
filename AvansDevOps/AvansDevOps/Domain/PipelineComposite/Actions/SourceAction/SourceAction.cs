using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.SourceAction
{
    public class SourceAction : CompositeComponent
    {
        public SourceAction(string name) : base(name)
        {
        }

        public override void AcceptVisitor(PipelineVisitor pipelineVisitor)
        {
            pipelineVisitor.VisitSource(this);
            base.AcceptVisitor(pipelineVisitor);
        }

        public override string ExecuteAction()
        {
            return "Running Source actions:";
        }
    }
}

using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.TestActions
{
    public class TestAction : CompositeComponent
    {
        public TestAction(string name) : base(name)
        {
        }

        public override string ExecuteAction()
        {
            return "Running tests";
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitTest(this);
            base.AcceptVisitor(visitor);
        }
    }
}

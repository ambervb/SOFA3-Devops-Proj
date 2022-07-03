using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.TestActions
{
    public class UnitTestAction : Component
    {

        public int _succeededCount { get; set; }
        public int _failedCount { get; set; }

        public UnitTestAction(string name) : base(name)
        {
        }

        public string ExecuteAction()
        {
            return "TEST: Running unit tests";
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitUnitTest(this);
        }
    }
}

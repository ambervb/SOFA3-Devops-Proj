using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.AnalyseActions
{
    public class AnalyseAction : CompositeComponent
    {
        public AnalyseAction(string name) : base(name)
        {
        }

        public override void AcceptVisitor(PipelineVisitor pipelineVisitor)
        {
            pipelineVisitor.VisitAnalyse(this);
            base.AcceptVisitor(pipelineVisitor);
        }

        public override string ExecuteAction()
        {
            return "Running Analyse actions:";
        }
    }
}

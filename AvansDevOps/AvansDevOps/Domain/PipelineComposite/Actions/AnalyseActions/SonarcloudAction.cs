using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.AnalyseActions
{
    public class SonarcloudAction : Component
    {
        public SonarcloudAction(string name) : base(name)
        {
        }

        public override void AcceptVisitor(PipelineVisitor pipelineVisitor)
        {
            pipelineVisitor.VisitSonarcloud(this);
        }

        public string ExecuteAction()
        {
            return "ANALYSE: Running Sonarcloud:";
        }
    }
}

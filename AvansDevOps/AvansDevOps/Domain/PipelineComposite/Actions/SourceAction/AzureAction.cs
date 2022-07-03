using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.SourceAction
{
    public class AzureAction : Component
    {
        public AzureAction(string name) : base(name)
        {
        }

        public string ExecuteAction()
        {
            return "SOURCE: Running azure\ngit pull urlNaarAzureRepo";
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitAzure(this);
        }
    }
}

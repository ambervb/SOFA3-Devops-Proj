using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.TestActions
{
    public class SeleniumAction : Component
    {
        public SeleniumAction(string name) : base(name)
        {
        }

        public string ExecuteAction()
        {
            return "TEST: Running Selenium";
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitSelenium(this);
        }
    }
}

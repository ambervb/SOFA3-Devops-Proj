using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Actions.SourceAction
{
    public class GithubAction : Component
    {
        public GithubAction(string name) : base(name)
        {
        }

        public string ExecuteAction()
        {
            return "SOURCE: Running Github\ngit pull urlNaarGitRepo";
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitGithub(this);
        }
    }
}

using AvansDevOps.Domain.PipelineComposite.Actions.AnalyseActions;
using AvansDevOps.Domain.PipelineComposite.Actions.SourceAction;
using AvansDevOps.Domain.PipelineComposite.Actions.TestActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.PipelineComposite.Visitors
{
    public class RunActionVisitor : PipelineVisitor
    {
        public override void VisitPipeline(Pipeline pipeline)
        {
            Console.WriteLine(pipeline.ExecuteAction());
        }

        public override void VisitSelenium(SeleniumAction seleniumAction)
        {
            Console.WriteLine(seleniumAction.ExecuteAction());
        }

        public override void VisitTest(TestAction testAction)
        {
            Console.WriteLine(testAction.ExecuteAction());
        }

        public override void VisitUnitTest(UnitTestAction unitTestAction)
        {
            Console.WriteLine(unitTestAction.ExecuteAction());
        }

        public override void VisitAnalyse(AnalyseAction analyseAction)
        {
            Console.WriteLine(analyseAction.ExecuteAction());
        }
        public override void VisitSonarcloud(SonarcloudAction sonarcloudAction)
        {
            Console.WriteLine(sonarcloudAction.ExecuteAction());
        }

        public override void VisitSource(SourceAction sourceAction)
        {
            Console.WriteLine(sourceAction.ExecuteAction());
        }

        public override void VisitAzure(AzureAction azureAction)
        {
            Console.WriteLine(azureAction.ExecuteAction());
        }

        public override void VisitGithub(GithubAction githubAction)
        {
            Console.WriteLine(githubAction.ExecuteAction());
        }
    }
}

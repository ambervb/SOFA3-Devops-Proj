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
    public abstract class PipelineVisitor
    {
        public abstract void VisitPipeline(Pipeline pipeline);
        // Test
        public abstract void VisitTest(TestAction testAction);
        public abstract void VisitUnitTest(UnitTestAction unitTestAction);
        public abstract void VisitSelenium(SeleniumAction seleniumAction);
        // Analyse
        public abstract void VisitAnalyse(AnalyseAction analysisAction);
        public abstract void VisitSonarcloud(SonarcloudAction sonarcloudAction);
        // Source
        public abstract void VisitSource(SourceAction sourceAction);
        public abstract void VisitAzure(AzureAction acureAction);
        public abstract void VisitGithub(GithubAction githubAction);
    }
}

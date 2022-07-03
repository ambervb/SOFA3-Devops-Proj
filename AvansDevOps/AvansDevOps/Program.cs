using AvansDevOps.Domain;
using AvansDevOps.Domain.ExportBehaviors;
using AvansDevOps.Domain.Factories;
using AvansDevOps.Domain.PipelineComposite.Actions.AnalyseActions;
using AvansDevOps.Domain.PipelineComposite.Actions.SourceAction;
using AvansDevOps.Domain.PipelineComposite.Actions.TestActions;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using AvansDevOps.Domain.UserRoles;

Console.WriteLine("Hello, World!");

//Create pipeline
//Pipeline pl = new Pipeline("name of pipeline");

//TestAction testActions = new TestAction("TestAction");
//UnitTestAction unitTestAction = new UnitTestAction("UnitTestAction");
//SeleniumAction seleniumAction = new SeleniumAction("SeleniumAction");
//pl.Add(testActions);
//testActions.Add(unitTestAction);
//testActions.Add(seleniumAction);

//AnalyseAction analyseAction = new AnalyseAction("Analyse");
//SonarcloudAction sonarcloudAction = new SonarcloudAction("Sonarcloud");
//pl.Add(analyseAction);
//analyseAction.Add(sonarcloudAction);

//SourceAction sourceAction = new SourceAction("SourceAction");
//AzureAction azureAction = new AzureAction("AzureAction");
//GithubAction githubAction = new GithubAction("GithubAction");
//pl.Add(sourceAction);
//sourceAction.Add(azureAction);
//sourceAction.Add(githubAction);


//PipelineVisitor runActionVisitor = new RunActionVisitor();
//Console.WriteLine("Executing the actions from here: " + "\n");
//pl.AcceptVisitor(runActionVisitor);

//Project project = new Project("KrokanteKrabApp", new UserFactory());
//project.AddUser("User1", "scrum-master");
//User scrumMaster = project._users[0];
//Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

//// Act
//string defaultReport = sprint.GenerateDefaultReport(new TxtExport());
//string defaultCommitReport = sprint.GenerateDefaultCommitReport(new TxtExport());
//string defaultAllReport = sprint.GenerateDefaultAllReport(new TxtExport());

//string defaultReport2 = sprint.GenerateDefaultReport(new PdfExport());
//string defaultCommitReport2 = sprint.GenerateDefaultCommitReport(new PdfExport());
//string defaultAllReport2 = sprint.GenerateDefaultAllReport(new PdfExport());

//Console.WriteLine(defaultReport);
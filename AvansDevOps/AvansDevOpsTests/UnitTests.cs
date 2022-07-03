using AvansDevOps.Domain;
using AvansDevOps.Domain.ActivityStates;
using AvansDevOps.Domain.BacklogItemStates;
using AvansDevOps.Domain.ExportBehaviors;
using AvansDevOps.Domain.Factories;
using AvansDevOps.Domain.NotificationAdapter;
using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Actions.AnalyseActions;
using AvansDevOps.Domain.PipelineComposite.Actions.SourceAction;
using AvansDevOps.Domain.PipelineComposite.Actions.TestActions;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using AvansDevOps.Domain.SprintStates;
using AvansDevOps.Domain.UserRoles;
using Moq;
using System;
using Xunit;

namespace AvansDevOpsTests
{
    public class UnitTests
    {
        // Project
        [Fact]
        public void ProjectCanBeCreated()
        {
            // Arrange


            // Act
            Project project = new Project("KrokanteKrabApp", new UserFactory());

            // Assert
            Assert.Empty(project._users);

        }

        [Fact]
        public void UserCanBeCreatedAndAddedToProject()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());

            // Act
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            project.AddUser("User3", "scrum-master");
            project.AddUser("User4", "product-owner");

            // Assert
            Assert.Equal(4, project._users.Count);
        }

        // Backlog item
        [Fact]
        public void BacklogItemCanBeCreatedAndAddedToProject()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());

            // Act
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddBacklogItem(new BacklogItem("Item2"));

            // Assert
            Assert.Equal(2, project._backlogItems.Count);
        }

        [Fact]
        public void BacklogItemCanBeLinkedToAUser()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User user = project._users[0];

            // Act
            backlogItem._user = user;

            // Assert
            Assert.Equal(user, backlogItem._user);
        }

        [Fact]
        public void BacklogStateTransitions_HappyFlow()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            BacklogItem backlogItem = project._backlogItems[0];
            backlogItem.AddActivity(new Activity("Task1"));
            User developer = project._users[0];
            User leadDeveloper = project._users[1];
            Activity activity = backlogItem._activities[0];

            // Act
            backlogItem.ToDoing();
            activity.ToInProgress();
            activity.ToDone();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();
            backlogItem.ToDone(leadDeveloper);

            // Assert
            Assert.IsType<DoneBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_HappyFlow_RftToTesting_NotSameDeveloper()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User developer1 = project._users[0];
            User developer2 = project._users[1];
            backlogItem._user = developer1;

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer1);

            // Assert
            Assert.IsType<ReadyForTestingBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_HappyFlow_TestedToDone_LeadDeveloperOnly()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User developer = project._users[0];
            User leadDeveloper = project._users[1];

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();
            backlogItem.ToDone(developer);

            // Assert
            Assert.IsType<TestedBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_HappyFlow_TestedToDone_ActivitiesMustBeDone()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            BacklogItem backlogItem = project._backlogItems[0];
            backlogItem.AddActivity(new Activity("Task1"));
            User developer = project._users[0];
            User leadDeveloper = project._users[1];
            Activity activity = backlogItem._activities[0];

            // Act
            backlogItem.ToDoing();
            activity.ToInProgress();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();
            backlogItem.ToDone(leadDeveloper);

            // Assert
            Assert.IsType<TestedBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_SadFlow_TestingToTodo()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User developer1 = project._users[0];
            User developer2 = project._users[1];
            backlogItem._user = developer1;

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer2);

            // Assert
            Assert.IsType<TestingBacklogItemState>(backlogItem._backlogItemState);

            backlogItem.ToTodo();

            Assert.IsType<TodoBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_SadFlow_TestedToRft()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User developer = project._users[0];
            User leadDeveloper = project._users[1];

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();

            // Assert
            Assert.IsType<TestedBacklogItemState>(backlogItem._backlogItemState);

            backlogItem.ToRft(leadDeveloper);

            Assert.IsType<ReadyForTestingBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_SadFlow_TestedToRft_LeadDeveloperOnly()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            BacklogItem backlogItem = project._backlogItems[0];
            User developer = project._users[0];
            User leadDeveloper = project._users[1];

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();

            // Assert
            Assert.IsType<TestedBacklogItemState>(backlogItem._backlogItemState);

            backlogItem.ToRft(developer);

            Assert.IsType<TestedBacklogItemState>(backlogItem._backlogItemState);
        }

        [Fact]
        public void BacklogStateTransitions_Invalid_Transitions()
        {
            // Arrange
            User developer = new LeadDeveloper("User1");
            User leadDeveloper = new LeadDeveloper("User2");
            var backlogItem1 = new BacklogItem("Item1");
            backlogItem1._backlogItemState = new DoneBacklogItemState();
            backlogItem1._user = developer;

            // Act
            backlogItem1.ToDone(leadDeveloper);
            backlogItem1.ToTodo();
            backlogItem1.ToDoing();
            backlogItem1.ToRft(leadDeveloper);
            backlogItem1.ToRft();
            backlogItem1.ToTesting(leadDeveloper);
            backlogItem1.ToTested();

            // Assert
            Assert.IsType<DoneBacklogItemState>(backlogItem1._backlogItemState);
        }

        // Activity
        [Fact]
        public void ActivityCanBeCreatedAndAddedToBacklogItem()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            BacklogItem backlogItem = project._backlogItems[0];

            // Act
            backlogItem.AddActivity(new Activity("Task1"));
            backlogItem.AddActivity(new Activity("Task2"));

            // Assert
            Assert.Equal(2, backlogItem._activities.Count);
        }

        [Fact]
        public void ActivityCanBeLinkedToAUser()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            Activity activity = new Activity("Task1");

            // Act
            activity._user = developer;

            // Assert
            Assert.Equal(developer, activity._user);
        }

        [Fact]
        public void ActivityStateTransitions_HappyFlow()
        {
            // Arrange
            Activity activity = new Activity("Task1");

            // Act
            activity.ToInProgress();
            activity.ToDone();

            // Assert
            Assert.IsType<DoneActivityState>(activity._activityState);
        }

        [Fact]
        public void ActivityStateTransitions_Invalid_Transitions()
        {
            // Arrange
            Activity activity = new Activity("Task1");
            activity._activityState = new DoneActivityState();

            // Act
            activity.ToInProgress();
            activity.ToDone();

            // Assert
            Assert.IsType<DoneActivityState>(activity._activityState);
        }

        // Sprint
        [Fact]
        public void SprintCanBeCreatedAndAddedToProject()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];

            // Act
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            project.AddSprint(new Sprint("Sprint2", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));

            // Assert
            Assert.Equal(2, project._sprints.Count);
        }

        [Fact]
        public void BacklogItemCanBeAddedToSprint()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddBacklogItem(new BacklogItem("Item1"));
            project.AddBacklogItem(new BacklogItem("Item2"));
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            BacklogItem backlogItem1 = project._backlogItems[0];
            BacklogItem backlogItem2 = project._backlogItems[1];

            // Act
            sprint.AddBacklogItem(backlogItem1);
            sprint.AddBacklogItem(backlogItem2);

            // Assert
            Assert.Equal(2, sprint._backlogItems.Count);
        }

        [Fact]
        public void SprintTransitions_HappyFlow_Pipeline()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._pipeline = new Pipeline("Pipeline1");

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();
            sprint.CloseSprint();

            // Assert
            Assert.IsType<ClosedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_HappyFlow_Review()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];

            // Act
            sprint.FinishSprint();
            sprint.StartReview();
            sprint.UploadReview();
            sprint.CloseSprint();

            // Assert
            Assert.IsType<ClosedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_HappyFlow_PipelineReview()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._pipeline = new Pipeline("Pipeline1");

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();
            sprint.StartReview();
            sprint.UploadReview();
            sprint.CloseSprint();

            // Assert
            Assert.IsType<ClosedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_SadFlow_PipelineReviewNotUploaded()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._pipeline = new Pipeline("Pipeline1");

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();
            sprint.StartReview();
            sprint.CloseSprint();

            // Assert
            Assert.IsType<ReviewSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_SadFlow_PipelineFailed()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._pipeline = new Pipeline("Pipeline1");

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();
            sprint.FinishSprint();

            // Assert
            Assert.IsType<FinishedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_SadFlow_CancelSprint()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];

            // Act
            sprint.FinishSprint();
            sprint.CloseSprint(scrumMaster);

            // Assert
            Assert.IsType<ClosedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintTransitions_SadFlow_CancelSprint_ScrumMasterOnly()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            project.AddUser("User2", "developer");
            User scrumMaster = project._users[0];
            User developer = project._users[1];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];

            // Act
            sprint.FinishSprint();
            sprint.CloseSprint(developer);

            // Assert
            Assert.IsType<FinishedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintStateTransitions_Invalid_Transitions()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._sprintState = new ClosedSprintState();

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();
            sprint.StartReview();
            sprint.CloseSprint();

            // Assert
            Assert.IsType<ClosedSprintState>(sprint._sprintState);
        }

        [Fact]
        public void SprintCannotBeChangesAfterFinished()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            sprint._pipeline = new Pipeline("Pipeline1");

            // Act
            sprint.FinishSprint();
            sprint.SetName("OtherSprint123");

            // Assert
            Assert.Equal("Sprint1", sprint._name);
        }

        // Pipeline
        [Fact]
        public void PipelineCanBeCreatedAndAddedToSprint()
        {
            //Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            Pipeline pl = new Pipeline("name of pipeline");

            //Act
            sprint._pipeline = pl;

            //Assert
            Assert.Equal(pl, sprint._pipeline);
        }

        [Fact]
        public void PipelineCanBeRun_ToUnitTest()
        {
            //Arrange
            var pipeline = new Pipeline("pipeline");
            var testAction = new TestAction("CC1");
            var unitTestAction = new UnitTestAction("C1");
            var pipelineVisitor = new Mock<PipelineVisitor>();

            pipeline.Add(testAction);
            testAction.Add(unitTestAction);

            //Act
            pipeline.AcceptVisitor(pipelineVisitor.Object);

            //Assert
            pipelineVisitor.Verify(x => x.VisitUnitTest(It.IsAny<UnitTestAction>()), Times.Once);
        }

        [Fact]
        public void PipelineCanBeRun_ToSelenium()
        {
            //Arrange
            var pipeline = new Pipeline("pipeline");
            var testAction = new TestAction("CC1");
            var seleniumAction = new SeleniumAction("C1");
            var pipelineVisitor = new Mock<PipelineVisitor>();

            pipeline.Add(testAction);
            testAction.Add(seleniumAction);

            //Act
            pipeline.AcceptVisitor(pipelineVisitor.Object);

            //Assert
            pipelineVisitor.Verify(x => x.VisitSelenium(It.IsAny<SeleniumAction>()), Times.Once);
        }

        [Fact]
        public void PipelineCanBeRun_ToGithub()
        {
            //Arrange
            var pipeline = new Pipeline("pipeline");
            var sourceAction = new SourceAction("CC1");
            var githubAction = new GithubAction("C1");
            var pipelineVisitor = new Mock<PipelineVisitor>();

            pipeline.Add(sourceAction);
            sourceAction.Add(githubAction);

            //Act
            pipeline.AcceptVisitor(pipelineVisitor.Object);

            //Assert
            pipelineVisitor.Verify(x => x.VisitGithub(It.IsAny<GithubAction>()), Times.Once);
        }

        [Fact]
        public void PipelineCanBeRun_ToAzure()
        {
            //Arrange
            var pipeline = new Pipeline("pipeline");
            var sourceAction = new SourceAction("CC1");
            var azureAction = new AzureAction("C1");
            var pipelineVisitor = new Mock<PipelineVisitor>();

            pipeline.Add(sourceAction);
            sourceAction.Add(azureAction);

            //Act
            pipeline.AcceptVisitor(pipelineVisitor.Object);

            //Assert
            pipelineVisitor.Verify(x => x.VisitAzure(It.IsAny<AzureAction>()), Times.Once);
        }

        [Fact]
        public void PipelineCanBeRun_ToSonarCloud()
        {
            //Arrange
            var pipeline = new Pipeline("pipeline");
            var analyseAction = new AnalyseAction("CC1");
            var sonarCloudAction = new SonarcloudAction("C1");
            var pipelineVisitor = new Mock<PipelineVisitor>();

            pipeline.Add(analyseAction);
            analyseAction.Add(sonarCloudAction);

            //Act
            pipeline.AcceptVisitor(pipelineVisitor.Object);

            //Assert
            pipelineVisitor.Verify(x => x.VisitSonarcloud(It.IsAny<SonarcloudAction>()), Times.Once);
        }

        [Fact]
        public void SprintRunsPipelineAtPipelineState()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            project.AddSprint(new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster));
            Sprint sprint = project._sprints[0];
            var pipelineMock = new Mock<Pipeline>("Pipeline1");
            sprint._pipeline = pipelineMock.Object;

            // Act
            sprint.FinishSprint();
            sprint.StartPipeline();

            // Assert
            pipelineMock.Verify(x => x.AcceptVisitor(It.IsAny<PipelineVisitor>()), Times.Once);
        }

        // Notification
        [Fact]
        public void UsersCanRecieveNotificationsThroughMultipleChannels()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            var adapterMock1 = new Mock<INotificationAdapter>();
            var adapterMock2 = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock1.Object);
            developer.AddNotificationAdapter(adapterMock2.Object);
            Thread thread = new Thread("Thread1", developer);

            // Act
            thread.NotifySubscriber();

            // Assert
            adapterMock1.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
            adapterMock2.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotificationSendWhen_BacklogItemToRft()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            var adapterMock = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            BacklogItem backlogItem = new BacklogItem("Item1");
            backlogItem.Subscribe(developer);

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotificationSendWhen_BacklogItemToTodo()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            project.AddUser("User1", "lead-developer");
            User developer = project._users[0];
            User leadDeveloper = project._users[1];
            var adapterMock = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            BacklogItem backlogItem = new BacklogItem("Item1");
            backlogItem.Subscribe(developer);

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(leadDeveloper);
            backlogItem.ToTodo();

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void NotificationSendWhen_SprintToClosed()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            project.AddUser("User2", "scrum-master");
            User developer = project._users[0];
            User scrumMaster = project._users[1];
            var adapterMock = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);
            sprint.Subscribe(developer);

            // Act
            sprint.FinishSprint();
            sprint.CloseSprint();

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotificationSendWhen_PipelineFails()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            var adapterMock = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            Pipeline pipeline = new Pipeline("Pipeline1");
            pipeline.Subscribe(developer);

            // Act
            pipeline._isSuccesful = false;
            pipeline.HandleFailure();

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotificationSendWhen_PipelineCanceled()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            var adapterMock = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            Pipeline pipeline = new Pipeline("Pipeline1");
            pipeline.Subscribe(developer);

            // Act
            pipeline.CancelPipeline();

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotificationSendWhen_ThreadRecievesReaction()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            project.AddUser("User2", "developer");
            User developer = project._users[0];
            User developer2 = project._users[1];
            var adapterMock = new Mock<INotificationAdapter>();
            var adapterMock2 = new Mock<INotificationAdapter>();
            developer.AddNotificationAdapter(adapterMock.Object);
            developer2.AddNotificationAdapter(adapterMock2.Object);
            Thread thread = new Thread("Thread1", developer);

            // Act
            thread.AddReaction(new Reaction("Reaction1"), developer2);
            thread.AddReaction(new Reaction("Reaction2"), developer);

            // Assert
            adapterMock.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Exactly(2));
            adapterMock2.Verify(x => x.SendNotification(It.IsAny<string>()), Times.Exactly(2));
        }

        // Thread
        [Fact]
        public void ThreadCanBeCreatedAndAddedToBacklogItem()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            BacklogItem backlogItem = new BacklogItem("Item1");
            Thread thread = new Thread("Thread1", developer);

            // Act
            backlogItem.AddThread(thread);

            // Assert
            Assert.NotEmpty(backlogItem._threads);
        }

        [Fact]
        public void ThreadWillBeClosedWhenBacklogItemGetsClosed()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            project.AddUser("User2", "lead-developer");
            User developer = project._users[0];
            User leadDeveloper = project._users[1];
            BacklogItem backlogItem = new BacklogItem("Item1");
            Thread thread = new Thread("Thread1", developer);
            Thread thread2 = new Thread("Thread2", developer);
            backlogItem.AddThread(thread);
            backlogItem.AddThread(thread2);

            // Act
            backlogItem.ToDoing();
            backlogItem.ToRft();
            backlogItem.ToTesting(developer);
            backlogItem.ToTested();
            backlogItem.ToDone(leadDeveloper);

            // Assert
            Assert.True(thread._isLocked);
            Assert.True(thread2._isLocked);
        }

        [Fact]
        public void ResponceCanBeCreatedAndAddedToThread()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "developer");
            User developer = project._users[0];
            Thread thread = new Thread("Thread1", developer);

            // Act
            thread.AddReaction(new Reaction("Reaction1"), developer);

            // Assert
            Assert.NotEmpty(thread._reactions);
        }

        // Report
        [Fact]
        public void ReportIsAssembledCorrectly_Txt_Default()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultReport = sprint.GenerateDefaultReport(new TxtExport());

            // Assert
            Assert.Equal("Sprint report:\r\nGenerated to TXT", defaultReport);
        }

        [Fact]
        public void ReportIsAssembledCorrectly_Txt_Commit()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultCommitReport = sprint.GenerateDefaultCommitReport(new TxtExport());

            // Assert
            Assert.Equal("Sprint report:\r\nA lot of commits have been done this sprint\r\nGenerated to TXT", defaultCommitReport);
        }

        [Fact]
        public void ReportIsAssembledCorrectly_Txt_All()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultAllReport = sprint.GenerateDefaultAllReport(new TxtExport());

            // Assert
            Assert.Equal("Sprint report:\r\nA lot of commits have been done this sprint\r\nAll backlog items were done this sprint\r\nGenerated to TXT", defaultAllReport);
        }

        [Fact]
        public void ReportIsAssembledCorrectly_Pdf_Default()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultReport = sprint.GenerateDefaultReport(new PdfExport());

            // Assert
            Assert.Equal("Sprint report:\r\nGenerated to PDF", defaultReport);
        }

        [Fact]
        public void ReportIsAssembledCorrectly_Pdf_Commit()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultCommitReport = sprint.GenerateDefaultCommitReport(new PdfExport());

            // Assert
            Assert.Equal("Sprint report:\r\nA lot of commits have been done this sprint\r\nGenerated to PDF", defaultCommitReport);
        }

        [Fact]
        public void ReportIsAssembledCorrectly_Pdf_All()
        {
            // Arrange
            Project project = new Project("KrokanteKrabApp", new UserFactory());
            project.AddUser("User1", "scrum-master");
            User scrumMaster = project._users[0];
            Sprint sprint = new Sprint("Sprint1", DateTime.Now, DateTime.Now, (ScrumMaster)scrumMaster);

            // Act
            string defaultAllReport = sprint.GenerateDefaultAllReport(new PdfExport());

            // Assert
            Assert.Equal("Sprint report:\r\nA lot of commits have been done this sprint\r\nAll backlog items were done this sprint\r\nGenerated to PDF", defaultAllReport);
        }
    }
}

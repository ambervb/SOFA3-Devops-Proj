using AvansDevOps.Domain.ExportBehaviors;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using AvansDevOps.Domain.Report;
using AvansDevOps.Domain.Report.ReportDecorator;
using AvansDevOps.Domain.SprintStates;
using AvansDevOps.Domain.SubscriberInterfaces;
using AvansDevOps.Domain.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public class Sprint
    {
        public string _name { get; private set; }
        public DateTime _startDate { get; set; }
        public DateTime _endDate { get; set; }
        public List<BacklogItem> _backlogItems { get; set; }
        public List<Developer> _developers { get; set; }
        public List<LeadDeveloper> _leadDevelopers { get; set; }
        public List<ProductOwner> _productOwners { get; set; }
        public ScrumMaster _scrumMaster { get; set; }
        public Pipeline _pipeline { get; set; }
        public SprintState _sprintState { get; set; }
        public bool _hasReview { get; set; }
        public bool _isLocked { get; set; }
        public List<ISprintSubscriber> _subscribers { get; set; }


        public Sprint(string name, DateTime startdate, DateTime endDate, ScrumMaster scrumMaster)
        {
            _name = name;
            _startDate = startdate;
            _endDate = endDate;
            _backlogItems = new List<BacklogItem>();
            _developers = new List<Developer>();
            _leadDevelopers = new List<LeadDeveloper>();
            _productOwners = new List<ProductOwner>();
            _scrumMaster = scrumMaster;
            _sprintState = new CreatedSprintState();
            _hasReview = false;
            _isLocked = false;
            _subscribers = new List<ISprintSubscriber>();
        }

        public void SetName(string name)
        {
            if (!_isLocked)
            {
                _name = name;
            }
        }

        public void AddBacklogItem(BacklogItem backlogItem)
        {
            _backlogItems.Add(backlogItem);
            backlogItem._sprint = this;
        }

        public void AddDeveloper(Developer developer)
        {
            _developers.Add(developer);
        }

        public void AddLeadDeveloper(LeadDeveloper leadDeveloper)
        {
            _leadDevelopers.Add(leadDeveloper);
        }

        public void AddProductOwner(ProductOwner productOwner)
        {
            _productOwners.Add(productOwner);
        }

        public void RunPipeline()
        {
            _pipeline.AcceptVisitor(new RunActionVisitor());
        }

        public void UploadReview()
        {
            _hasReview = true;
        }

        // Reports
        public string GenerateDefaultReport(IExportBehavior exportBehavior)
        {
            SprintReport sprintreport = new DefaultSprintReport();
            return exportBehavior.PrepareExport(sprintreport.GetReport());
        }

        public string GenerateDefaultCommitReport(IExportBehavior exportBehavior)
        {
            SprintReport sprintreport = new DefaultSprintReport();
            sprintreport = new CommitAmount(sprintreport);
            return exportBehavior.PrepareExport(sprintreport.GetReport());
        }

        public string GenerateDefaultAllReport(IExportBehavior exportBehavior)
        {
            SprintReport sprintreport = new DefaultSprintReport();
            sprintreport = new CommitAmount(sprintreport);
            sprintreport = new BacklogItemsDoneAmount(sprintreport);
            return exportBehavior.PrepareExport(sprintreport.GetReport());
        }

        // Notify
        public void Subscribe(ISprintSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(ISprintSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void NotifySubscriber(string message)
        {
            foreach (ISprintSubscriber subscriber in _subscribers)
            {
                subscriber.SprintUpdate(message);
            }
        }

        // Sprint states
        public void FinishSprint()
        {
            _sprintState.FinishSprint(this);
        }

        public void StartPipeline()
        {
            _sprintState.StartPipeline(this);
        }

        public void StartReview()
        {
            _sprintState.StartReview(this);
        }

        public void CloseSprint()
        {
            _sprintState.CloseSprint(this);
            NotifySubscriber("NOTIFICATION: Sprint has been closed");
        }

        public void CloseSprint(User user)
        {
            _sprintState.CloseSprint(this, user);
            NotifySubscriber("NOTIFICATION: Sprint has been closed");
        }
    }
}

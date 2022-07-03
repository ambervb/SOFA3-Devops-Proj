using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Domain.PipelineComposite;
using AvansDevOps.Domain.PipelineComposite.Visitors;
using AvansDevOps.Domain.SubscriberInterfaces;

namespace AvansDevOps.Domain
{
    public class Pipeline : CompositeComponent
    {
        public bool _isSuccesful { get; set; }
        public List<IPipelineSubscriber> _subscribers { get; set; }


        public Pipeline(string name) : base(name)
        {
            _subscribers = new List<IPipelineSubscriber>();
        }

        public override string ExecuteAction()
        {
            return "Pipeline Running";
            
        }

        public void Subscribe(IPipelineSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IPipelineSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void NotifySubscriber(string message)
        {
            foreach (IPipelineSubscriber subscriber in _subscribers)
            {
                subscriber.PipelineUpdate(message);
            }
        }

        public void HandleFailure()
        {
            if (!_isSuccesful)
            {
                //send notification
                NotifySubscriber("Pipeline failed");
            }
        }

        public void CancelPipeline()
        {
            NotifySubscriber("Pipeline canceled");
        }

        public override void AcceptVisitor(PipelineVisitor visitor)
        {
            visitor.VisitPipeline(this);
            base.AcceptVisitor(visitor);
        }
    }
}

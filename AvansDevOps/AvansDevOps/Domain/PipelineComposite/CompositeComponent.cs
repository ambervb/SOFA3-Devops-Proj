using AvansDevOps.Domain.PipelineComposite.Visitors;
using System;

namespace AvansDevOps.Domain.PipelineComposite
{

    public abstract class CompositeComponent : Component
    {
        private List<Component> _pipelineActions = new List<Component>();

        protected CompositeComponent(string name) : base(name)
        {
        }

        //Operation methods
        public virtual string ExecuteAction()
        {
            return "Executing action";
        }

        //Composite methods
        public virtual void Add(Component pipelineAction)
        {

            _pipelineActions.Add(pipelineAction);
        }

        public virtual void Remove(Component pipelineAction)
        {
            Console.WriteLine("ATTEMPT: tried to add child");
            _pipelineActions.Remove(pipelineAction);
        }

        public virtual Component GetChild(int i)
        {
            Console.WriteLine("ATTEMPT: tried to get child");
            return _pipelineActions[i];
        }

        public override void AcceptVisitor(PipelineVisitor pipelineVisitor)
        {
            foreach (Component pipelineAction in _pipelineActions)
            {
                pipelineAction.AcceptVisitor(pipelineVisitor);
            }
        }
    }
}

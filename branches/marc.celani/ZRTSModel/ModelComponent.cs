using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// An abstact representation of a piece of model, following the composite pattern.  Exposes a common interface for each piece of model.
    /// Each piece of model is observable, and can be visited.  When children are added, the component notifies its observers.
    /// 
    /// Whenever a component notifies its observers, it tells its parent to notify its observers as well.
    /// </summary>
    [Serializable()]
    public abstract class ModelComponent
    {
        // Composite Pattern members
        private ModelComponent container = null;

        
        private List<ModelComponent> children = new List<ModelComponent>();

        // Observer Pattern members
        [NonSerialized()]
        private List<ModelComponentObserver> observers = new List<ModelComponentObserver>();
        
        // Composite Pattern Interface
        public ModelComponent GetContainer()
        {
            return container;
        }

        public void SetContainer(ModelComponent composite)
        {
            ModelComponent tempContainer = container;
            if (container != null)
            {
                container.GetChildren().Remove(this);
            }
            container = composite;
            
            // Notify the new tree.
            NotifyAll();

            if (tempContainer != null)
            {
                // Notify the old tree.
                tempContainer.NotifyAll();
            }
        }

        public virtual List<ModelComponent> GetChildren()
        {
            return children;
        }

        public virtual void AddChild(ModelComponent child)
        {
            children.Add(child);

            // Handles the NotifyAll()
            child.SetContainer(this);
        }

        public virtual void RemoveChild(ModelComponent child)
        {
            children.Remove(child);
            child.SetContainer(null);
        }
        

        // Observer Pattern Interfaces
        public void RegisterObserver(ModelComponentObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(ModelComponentObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyAll()
        {
            if (observers != null)
            {
                foreach (ModelComponentObserver o in observers)
                {
                    o.notify(this);
                }
                if (container != null)
                {
                    container.NotifyAll();
                }
            }
        }

        // Visitor Pattern Interfaces
        public abstract void Accept(ModelComponentVisitor visitor);

        public void UnregisterAll()
        {
            observers = new List<ModelComponentObserver>();
        }

    }
}

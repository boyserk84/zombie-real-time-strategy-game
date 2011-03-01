using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    [Serializable()]
    abstract class ModelComponent
    {
        // Composite Pattern members
        private ModelComponent container = null;
        private List<ModelComponent> children = new List<ModelComponent>();

        // Observer Pattern members
        private List<ModelComponentObserver> observers = new List<ModelComponentObserver>();
        
        // Composite Pattern Interface
        public ModelComponent GetContainer()
        {
            return container;
        }

        public void SetContainer(ModelComponent composite)
        {
            container = composite;
        }

        public List<ModelComponent> GetChildren()
        {
            return children;
        }

        public void AddChild(ModelComponent child)
        {
            children.Add(child);
        }

        public void RemoveChild(ModelComponent child)
        {
            children.Remove(child);
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
            foreach (ModelComponentObserver o in observers)
            {
                o.notify(this);
            }
            if (container != null)
            {
                container.NotifyAll();
            }
        }

        // Visitor Pattern Interfaces
        public abstract void Accept(ModelComponentVisitor visitor);

    }
}

using ECS.Components;
using System;
using System.Collections.Generic;

namespace ECS
{
    public class Entity : IEqualityComparer<Entity>
    {

        public int ID { get; }
        private static int IdCounter = 0;
        private ISet<Component> components = new HashSet<Component>();
        private ISet<Type> componentsTypes = new HashSet<Type>();

        public Entity()
        {
            ID = IdCounter;
            IdCounter++;
        }

        public void AddComponent<T>(T component) where T:Component
        {
            components.Add(component);
            componentsTypes.Add(typeof(T));
        }

        public void RemoveComponent<T>(T component) where T:Component
        {
            components.Remove(component);
            componentsTypes.Remove(typeof(T));
        }

        public bool Equals(Entity x, Entity y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Entity obj)
        {
            return ID;
        }

        public bool Has(Type componentType)
        {
            return componentsTypes.Contains(componentType);
        }
    }
}

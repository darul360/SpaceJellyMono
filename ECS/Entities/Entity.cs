using System.Collections.Generic;

namespace ECS
{
    public class Entity: IEqualityComparer<Entity>
    {

        public int ID { get; }
        private static int IdCounter = 0;
        private ISet<Component> components = new HashSet<Component>();

        public Entity()
        {
            ID = IdCounter;
            IdCounter++;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            components.Remove(component);
        }

        public bool Equals(Entity x, Entity y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Entity obj)
        {
            return ID;
        }
    }
}

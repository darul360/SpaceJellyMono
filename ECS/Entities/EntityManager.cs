using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ECS.Entities
{
    public class EntityManager : IUpdateSystem
    {
        private ISet<Entity> Entities = new HashSet<Entity>();
        private ISet<Entity> AddedEntiesBuffer = new HashSet<Entity>();
        private ISet<Entity> RemovedEntiesBuffer = new HashSet<Entity>();
        private ISet<Entity> ChangedEntiesBuffer = new HashSet<Entity>();

        public event Action<Entity> OnEntityChange;
        public event Action<Entity> OnEntityRemove;
        public event Action<Entity> OnEntityAdd;


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            //Add entities
            Entities.UnionWith(AddedEntiesBuffer);
            foreach (Entity entity in AddedEntiesBuffer)
            {
                OnEntityAdd?.Invoke(entity);
            }

            AddedEntiesBuffer.Clear();

            //Remove entities
            Entities.ExceptWith(RemovedEntiesBuffer);
            foreach (Entity entity in RemovedEntiesBuffer)
            {
                OnEntityRemove?.Invoke(entity);
            }

            RemovedEntiesBuffer.Clear();

            //Update entities
            foreach (Entity entity in ChangedEntiesBuffer)
            {
                OnEntityChange?.Invoke(entity);
            }

            ChangedEntiesBuffer.Clear();
        }

        public void AddEntrity(Entity entity)
        {
            AddedEntiesBuffer.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            RemovedEntiesBuffer.Add(entity);
        }
    }
}

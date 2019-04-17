using ECS.Entities;
using ECS.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ECS
{
    public class World
    {
        private ISet<IUpdateSystem> updateSystems = new HashSet<IUpdateSystem>();
        private ISet<IDrawSystem> drawSystems = new HashSet<IDrawSystem>();

        internal EntityManager entityManager;

        public World()
        {
            entityManager = new EntityManager();
            AddSystem(entityManager);
        }

        public void AddSystem(ISystem system)
        {
            system.Initialize(this);

            if(system is IUpdateSystem updateSystem)
            {
                updateSystems.Add(updateSystem);
            }

            if(system is IDrawSystem drawSystem)
            {
                drawSystems.Add(drawSystem);
            }

            if(system is EntityUpdateSystem entityUpdateSystem)
            {
                entityManager.OnEntityAdd += entityUpdateSystem.AddEntityIfApplicable;
                entityManager.OnEntityChange += entityUpdateSystem.RemoveEntityIfNotApplicable;
                entityManager.OnEntityRemove += entityUpdateSystem.RemoveEntity;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(IUpdateSystem updateSystem in updateSystems)
            {
                updateSystem.Update(gameTime);
            }
        }
    }
}

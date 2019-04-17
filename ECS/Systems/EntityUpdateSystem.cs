using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ECS.Systems
{
    public abstract class EntityUpdateSystem : IUpdateSystem
    {
        internal ISet<Entity> AplicableEntities = new HashSet<Entity>();
        public ISet<Type> AplicableTypes = new HashSet<Type>();
        public ISet<Type> UnAplicableTypes = new HashSet<Type>();

        public void Dispose()
        {

        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            Begin();

            foreach (Entity entity in AplicableEntities)
            {
                Process(entity, gameTime);
            }

            End();
        }

        public virtual void Begin() { }
        public abstract void Process(Entity entity, GameTime gameTime);
        public virtual void End() { }

        public void AddEntityIfApplicable(Entity entity)
        {
            bool aplicableEntity = true;
            foreach (Type aplicableComponentType in AplicableTypes)
            {
                if (!entity.Has(aplicableComponentType))
                {
                    aplicableEntity = false;
                }
            }

            foreach (Type aplicableComponentType in UnAplicableTypes)
            {
                if (entity.Has(aplicableComponentType))
                {
                    aplicableEntity = false;
                }
            }

            if (aplicableEntity)
            {
                AplicableEntities.Add(entity);
            }
        }
    }
}

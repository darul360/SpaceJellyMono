using Microsoft.Xna.Framework;

namespace ECS
{
    interface IUpdateSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}

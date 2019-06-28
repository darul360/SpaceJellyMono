using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class FloatingTextRenderer : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Camera camera;

        private Queue<FloatingText> floatingTextObjects = new Queue<FloatingText>();

        public FloatingTextRenderer(Game game, SpriteBatch spriteBatch, Camera camera) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.camera = camera;

            spriteFont = game.Content.Load<SpriteFont>("WaterCounter");
        }
        public void Add(FloatingText floatingText)
        {
            if (!floatingTextObjects.Contains(floatingText))
                floatingTextObjects.Enqueue(floatingText);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 screenPosition = new Vector2();
            foreach(FloatingText floatingText in floatingTextObjects)
            {
                Vector3 projectedPosition = GraphicsDevice.Viewport.Project(floatingText.Position, camera.Projection, camera.View, floatingText.SourceWorldTransform);
                projectedPosition.X += floatingText.ScreenOffsetX;
                projectedPosition.Y += floatingText.ScreenOffsetY;
                screenPosition.X = projectedPosition.X + floatingText.ScreenOffsetX;
                screenPosition.Y = projectedPosition.Y + floatingText.ScreenOffsetY;
                spriteBatch.DrawString(spriteFont, floatingText.Content, screenPosition, floatingText.TextColor);
            }


        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (FloatingText floatingText in floatingTextObjects)
                floatingText.UpdateTime(gameTime.ElapsedGameTime);

                while(floatingTextObjects.Count != 0 && floatingTextObjects.Peek().OutOfTime)
                    floatingTextObjects.Dequeue();
        }
    }
}

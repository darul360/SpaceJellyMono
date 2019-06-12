using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.GameObjectComponents
{
    public class FloatingText : DrawableGameComponent
    {
        private SpriteFont font;
        private SpriteBatch spriteBatch;
        Game1 game;
        Vector2 position;
        public Transform transform;
        public String tekst;

        public FloatingText(Game1 game, Transform transform, String tekst) : base(game)
        {
            this.game = game;
            font = game.Content.Load<SpriteFont>("WaterCounter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.transform = transform;
            this.tekst = tekst;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            Vector3 tmp = game.GraphicsDevice.Viewport.Project(Vector3.Zero, game.camera.Projection, game.camera.View, Matrix.CreateTranslation(transform.translation));
            position.X = (int)tmp.X - 100;
            position.Y = (int)tmp.Y - 100;
            spriteBatch.DrawString(font, tekst, position, Color.Blue);
            spriteBatch.End();

        }
    }
}

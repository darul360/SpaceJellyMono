using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO.World
{
    public class WriteStats : DrawableGameComponent
    {
        Game1 game;
        private SpriteFont font;
        public int waterCounter = 0;
        private SpriteBatch spriteBatch;
        public WriteStats(Game1 game) : base(game)
        {
            this.game = game;
            font = game.exportContentManager().Load<SpriteFont>("WaterCounter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Water :" + waterCounter.ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

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
        Texture2D texture;
        public WriteStats(Game1 game) : base(game)
        {
            this.game = game;
            font = game.exportContentManager().Load<SpriteFont>("WaterCounter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("ui");
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(0, 0, 1920, 152), Color.White);
            spriteBatch.DrawString(font, game.resourcesStatistics.waterStats.ToString(), new Vector2(310, 60), Color.Aqua);
            spriteBatch.DrawString(font,  game.resourcesStatistics.bluePowderStats.ToString(), new Vector2(990, 60), Color.Blue);
            spriteBatch.DrawString(font,  game.resourcesStatistics.yellowPowderStats.ToString(), new Vector2(1600,60), Color.Yellow);
            spriteBatch.End();
               
        }
    }
}

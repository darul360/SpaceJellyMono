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
        Texture2D texture,texture2;
        public WriteStats(Game1 game) : base(game)
        {
            this.game = game;
            font = game.exportContentManager().Load<SpriteFont>("WaterCounter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("ui");
            texture2 = game.Content.Load<Texture2D>("downui");
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(0, 0, 1920, 152), Color.White);
            spriteBatch.Draw(texture2, new Rectangle(0, 870, 1920, 152), Color.White);
            spriteBatch.DrawString(font, game.resourcesStatistics.waterStats.ToString(), new Vector2(310, 60), Color.Aqua);
            spriteBatch.DrawString(font,  game.resourcesStatistics.bluePowderStats.ToString(), new Vector2(990, 60), Color.Blue);
            spriteBatch.DrawString(font,  game.resourcesStatistics.yellowPowderStats.ToString(), new Vector2(1600,60), Color.Yellow);

            game.resourcesStatistics.Refresh();
            spriteBatch.DrawString(font, game.resourcesStatistics.workers.ToString(), new Vector2(370,915), Color.Green);
            spriteBatch.DrawString(font, game.resourcesStatistics.warriors.ToString(), new Vector2(930, 915), Color.LightYellow);
            spriteBatch.DrawString(font, game.resourcesStatistics.enemies.ToString(), new Vector2(1560, 915), Color.RosyBrown);

            spriteBatch.DrawString(font, game.resourcesStatistics.selectedWorkers.ToString(), new Vector2(470, 915), Color.Red);
            spriteBatch.DrawString(font, game.resourcesStatistics.selectedWarriors.ToString(), new Vector2(1030, 915), Color.Red);
            spriteBatch.End();
               
        }
    }
}

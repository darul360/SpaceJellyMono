using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO.World
{
    public class WriteStats 
    {
        Game1 game;
        private SpriteFont font;
        public int waterCounter = 0;
        private SpriteBatch spriteBatch;
        Texture2D texture,texture2,texture3;
        public float timer = 45;
        const float TIMER =45;
        public WriteStats(Game1 game)
        {
            this.game = game;
            font = game.exportContentManager().Load<SpriteFont>("WaterCounter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture2 = game.Content.Load<Texture2D>("downui");
            texture3 = game.Content.Load<Texture2D>("WAVEWARNING");
        }
        public  void Draw(GameTime gameTime)
        {
            game.resourcesStatistics.Refresh();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0) { timer = TIMER; }
            spriteBatch.Begin();
            spriteBatch.Draw(texture2, new Rectangle(0, 930, 1920, 150), Color.White);
            spriteBatch.Draw(texture3, new Rectangle(0, 0, 300, 100), Color.White);
            if(timer >10)
            spriteBatch.DrawString(font,  timer.ToString().Substring(0,2), new Vector2(220, 25), Color.White);
            if(timer < 10)
                spriteBatch.DrawString(font, timer.ToString().Substring(0, 1), new Vector2(220, 25), Color.White);
            spriteBatch.DrawString(font, game.resourcesStatistics.waterStats.ToString(), new Vector2(1640, 975), Color.Black);
            spriteBatch.DrawString(font,  game.resourcesStatistics.bluePowderStats.ToString(), new Vector2(1230, 975), Color.Black);
            spriteBatch.DrawString(font,  game.resourcesStatistics.yellowPowderStats.ToString(), new Vector2(1450, 975), Color.Black);

            game.resourcesStatistics.Refresh();
            spriteBatch.DrawString(font, game.resourcesStatistics.workers.ToString(), new Vector2(870,975), Color.Black);
            spriteBatch.DrawString(font, game.resourcesStatistics.warriors.ToString(), new Vector2(330, 975), Color.Black);
            //spriteBatch.DrawString(font, game.resourcesStatistics.enemies.ToString(), new Vector2(1560, 915), Color.RosyBrown);

            spriteBatch.DrawString(font, game.resourcesStatistics.selectedWorkers.ToString(), new Vector2(960, 975), Color.Red);
            spriteBatch.DrawString(font, game.resourcesStatistics.selectedWarriors.ToString(), new Vector2(420, 975), Color.Red);
            spriteBatch.DrawString(font, "TAB - MAP(locate enemy bases there)", new Vector2(1020, 0), Color.Red);

            spriteBatch.End();
               
        }
    }
}

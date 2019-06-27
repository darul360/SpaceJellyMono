using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class WONLOSE:DrawableGameComponent
    {
        Game1 game1;
        Texture2D win, lose;
        SpriteBatch spriteBatch;
        public WONLOSE(Game1 game) : base(game)
        {
            this.game1 = game;
            win = game.Content.Load<Texture2D>("won");
            lose = game.Content.Load<Texture2D>("lost");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            if(game1.baza.GetHp() <= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(lose, new Rectangle(0, 0, 1920, 1080), Color.White);
                spriteBatch.End();
            }

            if (game1.baseEnemy.GetHp() <= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(win, new Rectangle(0, 0, 1920, 1080), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}

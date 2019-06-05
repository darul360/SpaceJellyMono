using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class UI : DrawableGameComponent
    {
        Game1 game1;
        Texture2D texture;
        SpriteBatch spriteBatch;
        public UI(Game1 game1) : base(game1)
        {
            this.game1 = game1;
            texture = game1.Content.Load<Texture2D>("ui");
            spriteBatch = new SpriteBatch(game1.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(0, 0, 1920, 152), Color.White);
            spriteBatch.End();
            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO.World
{
    class ShowInfoAboutBuilding : DrawableGameComponent
    {
        Texture2D texture;
        Game1 game1;
        SpriteBatch spriteBatch;
        public ShowInfoAboutBuilding(Game1 game1) : base(game1)
        {
            this.game1 = game1;
            this.spriteBatch = new SpriteBatch(game1.GraphicsDevice);
            texture = game1.Content.Load<Texture2D>("baseWARNING");
        }

        public override void Draw(GameTime gameTime)
        {
            Vector3 clickPos = game1.clickCooridantes.FindWhereClicked();
            GameObject temp = null;
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "baza")
                {
                    temp = go;
                }
            }

            if (Vector3.Distance(clickPos, temp.transform.translation) < 1.0f)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, new Rectangle((int)clickPos.X,(int)clickPos.Z, 400, 250), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);


        }


    }
}

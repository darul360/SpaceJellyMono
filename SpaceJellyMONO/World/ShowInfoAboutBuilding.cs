using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceJellyMONO.FSM;

namespace SpaceJellyMONO.World
{
    class ShowInfoAboutBuilding : DrawableGameComponent
    {
        Texture2D texture,texture2,texture3,texture4;
        Game1 game1;
        SpriteBatch spriteBatch;

        public ShowInfoAboutBuilding(Game1 game1) : base(game1)
        {
            this.game1 = game1;
            this.spriteBatch = new SpriteBatch(game1.GraphicsDevice);
            texture = game1.Content.Load<Texture2D>("baseWARNING");
            texture2 = game1.Content.Load<Texture2D>("platformWARNING");
            texture3 = game1.Content.Load<Texture2D>("mineWARNING");
            texture4 = game1.Content.Load<Texture2D>("waterpumpWARNING");
           

        }


        public override void Draw(GameTime gameTime)
        {
            
            Vector3 clickPos = game1.clickCooridantes.FindWhereClicked();
            GameObject temp = null,temp2 = null,temp3=null,temp4=null;
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "baza")
                {
                    temp = go;
                }
                if (go.GameTag == "platform")
                {
                    temp2 = go;
                }
                if (go.GameTag == "mine")
                {
                    temp3 = go;
                }
                if (go.GameTag == "waterpump")
                {
                    temp4 = go;
                }
            }

            if (temp != null)
            {
                if (Vector3.Distance(clickPos, temp.transform.translation) < 1.0f)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, new Rectangle((int)clickPos.X, (int)clickPos.Z, 400, 250), Color.White);
                    spriteBatch.End();
                    // player.Stop();
                }
                if (Vector3.Distance(clickPos, temp2.transform.translation) < 1.0f)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture2, new Rectangle((int)clickPos.X, (int)clickPos.Z, 400, 250), Color.White);
                    spriteBatch.End();
                }
                if (temp3 != null)
                    if (Vector3.Distance(clickPos, temp3.transform.translation) < 1.0f)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(texture3, new Rectangle((int)clickPos.X, (int)clickPos.Z, 400, 250), Color.White);
                        spriteBatch.End();
                    }
                if (temp4 != null)
                    if (Vector3.Distance(clickPos, temp4.transform.translation) < 1.0f)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(texture4, new Rectangle((int)clickPos.X, (int)clickPos.Z, 400, 250), Color.White);
                        spriteBatch.End();
                    }
            }
            base.Draw(gameTime);


        }


    }
}

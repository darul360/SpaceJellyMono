using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace SpaceJellyMONO.World
{
    public class DrawPowderSources:DrawableGameComponent
    {
        private Game1 game1;
        private int numberOfSources;
        private bool isGenerated = false;

        public DrawPowderSources(Game1 game1,int numberOfSources):base(game1)
        {
            this.game1 = game1;
            this.numberOfSources = numberOfSources;
        }

        public void generatePowderSources()
        {
            Random random = new Random();
            for(int i=0;i<this.numberOfSources;i++)
            {
                if (i % 2 == 0)
                {
                    GameObject gameObject = new GameObject("blueStones", game1, new Vector3(random.Next(50), 0, random.Next(50)), 0, 0, 0, 0.5f, false, "bluePowder");
                    game1.scene.AddSceneObject("bluePowder" + i.ToString(), gameObject);
                }

                else
                {
                    GameObject gameObject = new GameObject("yellowStones", game1, new Vector3(random.Next(50), 0, random.Next(50)), 0, 0, 0, 0.5f, false, "yellowPowder");
                    game1.scene.AddSceneObject("yellowPowder" + i.ToString(), gameObject);
                }
            }
            isGenerated = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if(!isGenerated)
            {
                generatePowderSources();
            }
            base.Draw(gameTime);
        }
    }
}

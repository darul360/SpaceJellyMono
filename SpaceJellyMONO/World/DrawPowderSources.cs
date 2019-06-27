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

        public bool isLocationBlocked(int x,int y)
        {
            foreach(GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.transform.translation.X == x && go.transform.translation.Z == y) return true;
            }
            return false;
        }

        public void generatePowderSources()
        {
            Random random = new Random();
            for (int i = 0; i < this.numberOfSources; i++)
            {
                int x1 = random.Next(35); int x2 = random.Next(35);
                do { x1 = random.Next(35); x2 = random.Next(35); } while ((x1 == 0 || x1 == 100) && (x2 == 0 && x2 == 100) && isLocationBlocked(x1, x2));
                GameObject gameObject = new GameObject("blueStoneSource", game1, new Vector3(x1, -0.1f, x2), -1.57f, 0, 0, 0.03f, false, "bluePowder", 0.03f * 0.9f);
                game1.scene.AddSceneObject("bluePowder" + i.ToString(), gameObject);

                int x3 = (int)(random.NextDouble() * (65 - 35) + 35); int x4 = (int)(random.NextDouble() * (65 - 35) + 35);
                do { x3 = (int)(random.NextDouble() * (65 - 35) + 35); x4 = (int)(random.NextDouble() * (65 - 35) + 35); } while ((x1 == 0 || x1 == 100) && (x2 == 0 || x2 == 100) && isLocationBlocked(x1, x2));
                GameObject gameObject2 = new GameObject("yellowStoneSource", game1, new Vector3((float)x3, -0.1f, (float)x4), -1.57f, 0, 0, 0.03f, false, "yellowPowder", 0.03f * 0.9f);
                game1.scene.AddSceneObject("yellowPowder" + i.ToString(), gameObject2);
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

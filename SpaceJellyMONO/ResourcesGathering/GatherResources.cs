using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace SpaceJellyMONO.ResourcesGathering
{
    public class GatherResources:GameComponent
    {
        Game1 game1;
        float timer = 3;
        const float TIMER = 3;
        GameObject gameObject;

        public GatherResources(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }

        private void findSpecificResourceToGather(GameTime gameTime)
        {
            foreach(GameObject powderSource in game1.powderSourcesRepository.getRepo())
            {
                foreach(GameObject worker in game1.gameObjectsRepository.getRepo())
                {
                    if (worker.GameTag == "worker")
                    {
                        if (powderSource.collider.Intersect(worker.collider))
                        {
                            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                            timer -= elapsed;
                            worker.isGameObjectMovable = false;
                            Debug.WriteLine(timer);
                                if (timer < 0)
                                {
                                    game1.writeStats.waterCounter += 15;
                                    timer = TIMER;
                                    worker.isGameObjectMovable = true;
                                    gameObject = powderSource;
                                }
                        }
                    }
                }
                
            }
            if (gameObject != null)
            {
                string tempKey = game1.scene.FindKeyOfObject(gameObject);
                game1.scene.DeleteSceneObject(tempKey);
                gameObject = null;
            }
        }


        public override void Update(GameTime gameTime)
        {
            findSpecificResourceToGather(gameTime);
            base.Update(gameTime);
        }
    }
}

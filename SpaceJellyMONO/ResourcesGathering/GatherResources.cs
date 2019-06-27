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
        List<Tuple<GameObject,GameObject>> tempBlocked = new List<Tuple<GameObject, GameObject>>();

        public GatherResources(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }

        private bool isAtList(GameObject powder)
        {
            if( tempBlocked != null)
            {
                foreach(Tuple<GameObject,GameObject> tuple in tempBlocked)
                {
                    if (tuple.Item1 == powder) return true;
                }
            }
            return false;
        }


        private void findSpecificResourceToGather(GameTime gameTime)
        {
            foreach (GameObject powderSource in game1.powderSourcesRepository.getRepo())
            {
                foreach (GameObject worker in game1.gameObjectsRepository.getRepo())
                {
                    if (worker.GameTag == "worker")
                    {
                        if (powderSource.collider.Intersect(worker.collider))
                        {
                            if (!isAtList(powderSource))
                            {
                                tempBlocked.Add(new Tuple<GameObject, GameObject>(powderSource, worker));
                            }
                        }
                    }
                }
            }
        }

        private Tuple<GameObject,GameObject> findTuple(GameObject powder)
        {
            foreach (Tuple<GameObject, GameObject> t in tempBlocked)
            {
                if (t.Item1 == powder) return t;
            }
            return null;
        }

        private void GatherPowder(GameTime gameTime)
        {
            foreach(Tuple<GameObject,GameObject> t in tempBlocked)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                t.Item2.isGameObjectMovable = false;
                if (timer < 0)
                {
                    if (t.Item1.GameTag == "bluePowder")
                        game1.resourcesStatistics.bluePowderStats += 10;

                    if (t.Item1.GameTag == "yellowPowder")
                        game1.resourcesStatistics.yellowPowderStats += 10;

                    t.Item2.isGameObjectMovable = true;
                    gameObject = t.Item1;
                    timer = TIMER;
                }
            }

            if (gameObject != null)
            {
                string tempKey = game1.scene.FindKeyOfObject(gameObject);
                game1.scene.DeleteSceneObject(tempKey);
                game1.powderSourcesRepository.RemoveFromRepo(gameObject);
                tempBlocked.Remove(findTuple(gameObject));
                gameObject = null;
            }
        }


        public override void Update(GameTime gameTime)
        {
            findSpecificResourceToGather(gameTime);
            GatherPowder(gameTime);
            base.Update(gameTime);
        }
    }
}

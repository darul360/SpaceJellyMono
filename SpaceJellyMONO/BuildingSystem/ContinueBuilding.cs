using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceJellyMONO.BuildingSystem
{
    public class ContinueBuilding : GameComponent
    {
        Game1 game;
        GameObject gameObject,tempGameObject,tempGameObject2;
        int waterPumpCounter = 0;
        bool build = false,build2=false;
        public ContinueBuilding(Game1 game) : base(game)
        {
            this.game = game;
        }


        private void BuildWaterPump(int x,int y,int z)
        {
            gameObject = new GameObject("WaterPump", game, new Vector3(x, y, z), 4.75f, 7.8f, 0, 0.03f, false, "waterpump", 0.03f*0.9f);
            game.scene.AddSceneObject("waterPump" + waterPumpCounter.ToString(), gameObject);
        }

        private void BuildMine(int x, int y, int z)
        {
            gameObject = new GameObject("kopalnia", game, new Vector3(x, y, z), 0, 7.8f, 0, 0.03f, false, "mine", 0.03f * 0.9f);
            game.scene.AddSceneObject("mine" + waterPumpCounter.ToString(), gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject building in game.gameObjectsRepository.getRepo())
            {
                if (building.GameTag == "firstPartOfBuilding")
                {
                    foreach (GameObject worker in game.gameObjectsRepository.getRepo())
                    {
                        if (worker.GameTag == "worker" && worker.collider.Intersect(building.collider))
                        {
                            build = true;
                            tempGameObject = building;
                            tempGameObject2 = worker;
                            break;
                        }
                        if (worker.GameTag == "warrior" && worker.collider.Intersect(building.collider))
                        {
                            build2 = true;
                            tempGameObject = building;
                            tempGameObject2 = worker;
                            break;
                        }
                    }
                }
            }
            if (build)
            {
                BuildWaterPump((int)tempGameObject.transform.translation.X, (int)tempGameObject.transform.translation.Y, (int)tempGameObject.transform.translation.Z);
                game.scene.DeleteSceneObject(game.scene.FindKeyOfObject(tempGameObject));
                string workerKey = game.scene.FindKeyOfObject(tempGameObject2);     
                if (workerKey != null)                                  
                    game.scene.DeleteSceneObject(workerKey);
                waterPumpCounter++;
                build = false;
            }

            if (build2)
            {
                BuildMine((int)tempGameObject.transform.translation.X, (int)tempGameObject.transform.translation.Y, (int)tempGameObject.transform.translation.Z);
                game.scene.DeleteSceneObject(game.scene.FindKeyOfObject(tempGameObject));
                string workerKey = game.scene.FindKeyOfObject(tempGameObject2);
                if (workerKey != null)
                    game.scene.DeleteSceneObject(workerKey);
                waterPumpCounter++;
                build2 = false;
            }
            game.gameObjectsRepository.RemoveFromRepo(tempGameObject);
        }
    }
}

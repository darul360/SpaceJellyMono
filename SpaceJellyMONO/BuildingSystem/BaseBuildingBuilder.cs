using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceJellyMONO.BuildingSystem
{
    public class BaseBuildingBuilder: DrawableGameComponent
    {
        private Game1 game1;
        private GameObject gameObject, tempGameObject;
        private MouseState lastMouseState = new MouseState();
        private bool isCreated = false;
        private int buldingCounter = 0;

        public BaseBuildingBuilder(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }

        private void createBaseBuilding()
        {
            Vector3 clickLocation = game1.clickCooridantes.FindWhereClicked();
            Vector3 integerValues = new Vector3((int)Math.Round(clickLocation.X), (int)Math.Round(clickLocation.Y), (int)Math.Round(clickLocation.Z));
            gameObject = new GameObject("wood-pile", game1, integerValues, 30f, 0f, 0f, 0.009f, false, "baseBuilding");
            gameObject.buildingFlag = true;
            game1.scene.AddSceneObject("baseBuilding"+ buldingCounter.ToString(), gameObject);

        }

        public override void Update(GameTime gameTime)
        {
                MouseState currentState = Mouse.GetState();
                if (currentState.MiddleButton == ButtonState.Pressed &&
                     lastMouseState.MiddleButton == ButtonState.Released)
                {
                    if (isCreated == false)
                    {
                        createBaseBuilding();
                        isCreated = true;
                    }
                    else
                    {
                        game1.scene.DeleteSceneObject("baseBuilding"+ buldingCounter.ToString());
                        isCreated = false;
                    }

                }
                lastMouseState = currentState;

                if (isCreated)
                {
                    foreach (GameObject go in game1.gameObjectsRepository.getRepo())
                    {
                        if (gameObject.collider.Intersect(go.collider))
                        {
                            if (go.GameTag == "worker")
                            {
                                gameObject.buildingFlag = false;                        //zmiana przezroczystości obiektu
                                string workerKey = game1.scene.FindKeyOfObject(go);     //znajdź klucz obiektu w repo rysowania
                                if (workerKey != null)                                  //overcode
                                    game1.scene.DeleteSceneObject(workerKey);           //usuwam workera z potoku renderingu
                                buldingCounter++;                                       //inkrementacja id w celu umożliwienia stworzenia nowej konstrukcji podstawowej
                                isCreated = false;                                      //pozwala
                                tempGameObject = go;                                    //zgarniam obiekt do tempa bo foreach się sypyie
                                gameObject.GameTag = "firstPartOfBuilding";             //zmiana tagu
                                gameObject.sceneID = "baseBuilding" + buldingCounter.ToString();
                                break;
                            }

                        }
                    }
                    game1.gameObjectsRepository.RemoveFromRepo(tempGameObject);        //usuwam workera z repo
                }
        }
    }
}

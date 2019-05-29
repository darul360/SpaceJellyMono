using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO.BuildingSystem
{
    public class BaseBuildingBuilder: DrawableGameComponent
    {
        private Game1 game1;
        private Collider collider;
        private GameObject gameObject;
        private MouseState lastMouseState = new MouseState();
        private bool isCreated = false;
        public BaseBuildingBuilder(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }

        private void createBaseBuilding()
        {
            Vector3 clickLocation = game1.clickCooridantes.FindWhereClicked();
            Vector3 integerValues = new Vector3((int)Math.Round(clickLocation.X), (int)Math.Round(clickLocation.Y), (int)Math.Round(clickLocation.Z));
            gameObject = new GameObject("wood-pile", game1, integerValues, 30f, 0f, 0f, 0.009f, false,"worker");
            gameObject.GameTag = "baseBuilding";
            collider = new Collider(gameObject);
            game1.scene.AddSceneObject("baseBuilding", gameObject);

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
                    game1.scene.DeleteSceneObject("baseBuilding");
                    isCreated = false;
                }

            }
            lastMouseState = currentState;

            if (isCreated)
            {
                foreach(GameObject gameObject in game1.gameObjectsRepository.getRepo())
                {
                    if (gameObject.collider.Intersect(gameObject.collider)){
                        if(gameObject.GameTag == "worker")
                             Debug.WriteLine("budowanie");
                    }

                }
            }

        }

        public override void Draw(GameTime gameTime)
        {

        }


    }
}

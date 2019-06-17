using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO.World
{
    public class GenerateWorker :DrawableGameComponent
    {
        private Game1 game1;
        private MouseState lastMouseState = new MouseState();
        int i = 0;
        GameObject temp;
        public GenerateWorker(Game1 game1) : base(game1)
        {
            this.game1 = game1;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentState = Mouse.GetState();
            Vector3 clickPos;
            if (currentState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                clickPos = game1.clickCooridantes.FindWhereClicked();
                foreach(GameObject go in game1.gameObjectsRepository.getRepo())
                {
                    if(go.GameTag == "baza")
                    {
                        temp = go;
                    }
                }

            if (Vector3.Distance(clickPos, temp.transform.translation) < 1.0f)
                {
                    Random random = new Random();
                    if (game1.resourcesStatistics.bluePowderStats >= 5 && game1.resourcesStatistics.waterStats >= 5)
                    {
                        GameObject worker = new Jelly("jelly_blue", game1, new Vector3(temp.transform.translation.X + random.Next(-2, 2), 0, temp.transform.translation.Z + random.Next(-2, 2)), -1.57f, 0, 0, 0.5f, true, "worker",0.45f);
                        game1.scene.AddSceneObject("worker_X" + i.ToString(), worker);
                        worker.finateSatemachine = game1.animateJelly;
                        game1.resourcesStatistics.bluePowderStats -= 5;
                        game1.resourcesStatistics.waterStats -= 5;
                            i++;
                    }
                }
            }
            lastMouseState = currentState;
            base.Update(gameTime);
        }
    }
}

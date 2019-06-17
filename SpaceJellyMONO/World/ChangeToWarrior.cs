using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceJellyMONO.UnitsFolder;

namespace SpaceJellyMONO.World
{
    public class ChangeToWarrior : GameComponent
    {

        Game1 game;
        GameObject platform;
        bool change = false;
        int i = 0;
        public ChangeToWarrior(Game1 game,GameObject platform) : base(game)
        {
            this.game = game;
            this.platform = platform;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (platform != null)
            {
                GameObject temp = null;
                foreach (GameObject go in game.gameObjectsRepository.getRepo())
                {
                    if (go.GameTag == "worker")
                        if (platform.collider.Intersect(go.collider)) { temp = go; change = true; }
                }

                if (change)
                {
                   // GameObject gameOBJ = ;
                    game.scene.AddSceneObject("warrior" + i + "siemanko", new Warrior("jelly_yellow", game, temp.transform.translation, -1.57f, 0, 0, 0.5f, true, "warrior", 0.45f) {finateSatemachine = game.animateJelly });
                    string workerKey = game.scene.FindKeyOfObject(temp);     //znajdź klucz obiektu w repo rysowania
                    if (workerKey != null)                                  //overcode
                        game.scene.DeleteSceneObject(workerKey);
                    game.gameObjectsRepository.RemoveFromRepo(temp);
                    change = false;
                }
            }
           
        }
    }
}

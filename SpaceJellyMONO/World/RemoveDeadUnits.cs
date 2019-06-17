using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceJellyMONO.World
{
    class RemoveDeadUnits:GameComponent
    {
        Game1 game1;
        GameObject temp;
        public RemoveDeadUnits(Game1 game):base(game)
        {
            this.game1 = game;
        }

        public override void Update(GameTime gameTime)
        {
            foreach(GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GetHp() == 0 &&( go.GameTag=="enemy" || go.GameTag=="warrior" || go.GameTag =="worker" || go.GameTag == "spawn" || go.GameTag == "baza")) temp = go;
            }
           
            if (temp != null)
            {
                string workerKey = game1.scene.FindKeyOfObject(temp);
                if (workerKey != null)
                    game1.scene.DeleteSceneObject(workerKey);
                game1.gameObjectsRepository.RemoveFromRepo(temp);
                temp = null;
            }
            base.Update(gameTime);
        }
    }
}
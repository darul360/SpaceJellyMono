using Microsoft.Xna.Framework;
using SpaceJellyMONO.Units;
using SpaceJellyMONO.UnitsFolder;
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
        GameObject temp,temp2;
        public RemoveDeadUnits(Game1 game):base(game)
        {
            this.game1 = game;
        }

        public override void Update(GameTime gameTime)
        {
            foreach(GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GetHp() <= 0 &&( go.GameTag=="enemy"|| go.GameTag =="worker" || go.GameTag == "warrior" || go.GameTag == "spawn" || go.GameTag == "baza")) temp = go;
            }
           


            if (temp != null)
            {
                string workerKey = game1.scene.FindKeyOfObject(temp);
                if (workerKey != null)
                    game1.scene.DeleteSceneObject(workerKey);
                game1.gameObjectsRepository.RemoveFromRepo(temp);
                if (temp.GameTag == "warrior") game1.warriorsRepository.RemoveFromRepo(temp);
                if(temp.GameTag == "enemy") game1.enemiesRepository.RemoveFromRepo(temp);
                Unit selectableUnit = temp as Unit;
                game1.selectedObjectsRepository.RemoveFromRepo(selectableUnit);
                temp = null;
            }

            foreach (Warrior go in game1.warriorsRepository.getRepo())
            {
                if (go.GetHp() <= 0)
                    temp2 = go;
            }

            if(temp2 != null)
            {
               
                game1.warriorsRepository.RemoveFromRepo(temp2);
            }

            base.Update(gameTime);
        }
    }
}
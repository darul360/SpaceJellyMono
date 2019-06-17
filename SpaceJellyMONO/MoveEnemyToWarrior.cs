using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace SpaceJellyMONO
{
    public class MoveEnemyToWarrior:GameComponent
    {
        Game1 game1;
        public MoveEnemyToWarrior(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }

        public override void Update(GameTime gameTime)
        {
            //foreach(GameObject go in game1.gameObjectsRepository.getRepo())
            //{
            //    if (go.GameTag == "enemy")
            //    {
            //        foreach (GameObject go2 in game1.gameObjectsRepository.getRepo())
            //        {
            //            if(go2.GameTag == "warrior" /*|| go2.GameTag == "worker"*/)
            //            {
            //               if(Vector3.Distance(go2.transform.translation,go.transform.translation)<6.0f)
            //                {
            //                    go.isMoving = true;
            //                    go.targetX = (int)go2.transform.translation.X;
            //                    go.targetY = (int)go2.transform.translation.Z;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            base.Update(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceJellyMONO.GameObjectComponents;
using SpaceJellyMONO.UnitsFolder;

namespace SpaceJellyMONO
{
    public class MoveEnemyToWarrior:GameComponent
    {
        Game1 game1;
        SoundEffect effect;
        float timer = 1f;
        const float TIMER = 1f;
        GameObject temp,temp2;
        public MoveEnemyToWarrior(Game1 game1):base(game1)
        {
            this.game1 = game1;
            Warrior warrior = new Warrior("jelly_yellow", game1, new Vector3(1000, 1000, 10000), 0, 0, 0, 0.00001f, false, "daddsa", 1);
            game1.warriorsRepository.AddToRepo(warrior);
        }

        public void findActiveWarriorAround()
        {
            foreach (Enemy go in game1.enemiesRepository.getRepo())
            {
                foreach (Warrior go2 in game1.warriorsRepository.getRepo())
                {


                   if (Vector3.Distance(go.transform.translation, go2.transform.translation) < 6.0f && go2.GetHp()>=0)
                   {
                       go.targetX = (int)go2.moveObject.moveX;
                       go.targetY = (int)go2.moveObject.moveZ;
                       go.isMoving = true;
                       go.moveObject.isThatFirstStep = true;
                       temp = go;
                       temp2 = go2;
                   }


                    else
                    {
                       go.targetX = 17;
                       go.targetY = 15;
                       go.isMoving = true;
                       go.moveObject.isThatFirstStep = true;
                        if (Vector3.Distance(new Vector3(go.moveObject.moveX, 0, go.moveObject.moveZ), new Vector3(17, 0, 15)) < 2)
                        {
                            go.transform.translation = new Vector3(17, 0, 15);
                            go.isMoving = false;
                        }
                    }
                }

            }
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                findActiveWarriorAround();
                timer = TIMER;
            }

            /*
            base.Update(gameTime);
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if(timer < 0)
            {
                findPairs();
                timer = TIMER;
            }

            base.Update(gameTime);
            float elapsed2 = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer2 -= elapsed2;
            if (timer2 < 0)
            {
                //moveTuplesToEachOther(gameTime);
               // renewMovingToBase();
                timer2 = TIMER2;
            }

            /*if (timer < 0)
            {
                if (go.isEnemyMovingFromSpawn)
                {
                    go.targetX = (int)Math.Round(go2.moveObject.moveX);
                    go.targetY = (int)Math.Round(go2.moveObject.moveZ);
                    go.moveObject.isThatFirstStep = true;
                    go.isEnemyMovingFromSpawn = false;

                }

                go.moveObject.Move((float)gameTime.ElapsedGameTime.TotalMilliseconds, effect, (int)Math.Round(go2.moveObject.moveX), (int)Math.Round(go2.moveObject.moveZ));
                go.moveObject.isThatFirstStep = true;
                timer = TIMER;
            }*/


        }
    }
}

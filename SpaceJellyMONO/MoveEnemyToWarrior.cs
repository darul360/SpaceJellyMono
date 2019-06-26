using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceJellyMONO
{
    public class MoveEnemyToWarrior:GameComponent
    {
        Game1 game1;
        SoundEffect effect;
        float timer = 0.5f;
        const float TIMER = 0.5f;
        List<Tuple<GameObject,GameObject>> pairs;
        public MoveEnemyToWarrior(Game1 game1):base(game1)
        {
            this.game1 = game1;
            pairs = new List<Tuple<GameObject, GameObject>>();
        }

        public void findActiveWarriorAround()
        {
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "enemy")
                {
                    foreach (GameObject go2 in game1.gameObjectsRepository.getRepo())
                    {
                        if (go2.GameTag == "warrior")
                        {

                            if (Vector3.Distance(go.transform.translation, go2.transform.translation) < 3.0f && go2.GetHp()>=0)
                            {
                                go.targetX = (int)go2.moveObject.moveX;
                                go.targetY = (int)go2.moveObject.moveZ;
                                go.isMoving = true;
                                go.moveObject.isThatFirstStep = true;
                            }
                            else
                            {
                                go.targetX = 15;
                                go.targetY = 15;
                                go.isMoving = true;
                                go.moveObject.isThatFirstStep = true;
                            }
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

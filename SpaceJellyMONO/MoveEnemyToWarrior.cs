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
        float timer = 40;
        const float TIMER = 40;
        bool block1 = false, block2 = false;
        public MoveEnemyToWarrior(Game1 game1):base(game1)
        {
            this.game1 = game1;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer -= elapsed;
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "enemy")
                {
                    foreach (GameObject go2 in game1.gameObjectsRepository.getRepo())
                    {
                        if (go2.GameTag == "warrior" /*|| go2.GameTag == "worker"*/)
                        {
                            if (Vector3.Distance(go.transform.translation, go2.transform.translation) < 6.0f)
                            {
                                if (timer < 0)
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
                                    if (go2.moveObject.moveZ < go.moveObject.moveZ)
                                    {
                                        go.targetY = go.targetY - 1;
                                        go.transform.YRotation = 1.57f;
                                    }
                                    if (go2.moveObject.moveZ > go.moveObject.moveZ)
                                    {
                                        go.targetY = go.targetY + 1;
                                        go.transform.YRotation = 11;
                                    }

                                    timer = TIMER;
                                }
                            }
                        }
                    }
                }
            }
            
        }
    }
}

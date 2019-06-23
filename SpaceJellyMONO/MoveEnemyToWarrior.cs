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
        float timer = 2;
        float timer2 = 40;
        const float TIMER = 2;
        const float TIMER2 = 40;
        bool block1 = false, block2 = false;
        List<Tuple<GameObject,GameObject>> pairs;
        public MoveEnemyToWarrior(Game1 game1):base(game1)
        {
            this.game1 = game1;
            pairs = new List<Tuple<GameObject, GameObject>>();
        }

        public void findPairs()
        {
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "enemy")
                {
                    foreach (GameObject go2 in game1.gameObjectsRepository.getRepo())
                    {
                        if (go2.GameTag == "warrior")
                        {
                            if (Vector3.Distance(go.transform.translation, go2.transform.translation) < 6.0f)
                            {
                                if(!isTupleAtList(new Tuple<GameObject, GameObject>(go,go2)))
                                    pairs.Add(new Tuple<GameObject, GameObject>(go, go2));
                            }
                        }
                    }
                }
            }
        }

        public bool isTupleAtList(Tuple<GameObject,GameObject> T)
        {
            foreach(Tuple<GameObject, GameObject> tuple in pairs)
            {
                if (T == tuple) return true;
                if (T.Item1 == tuple.Item1) return true;
            }
            return false;
        }

        public void renewMovingToBase()
        {
            foreach (Tuple<GameObject, GameObject> tuple in pairs)
            {
                if (tuple.Item1 == null) pairs.Remove(tuple);
                if(tuple.Item2.GetHp()<0)
                {
                    pairs.Remove(tuple);
                    tuple.Item2.isEnemyMovingFromSpawn = true;
                    tuple.Item2.targetX = 17;
                    tuple.Item2.targetY = 15;
                    tuple.Item2.isMoving = true;
                    tuple.Item2.moveObject.isThatFirstStep = true;
                }
            }
        }

        public void moveTuplesToEachOther(GameTime gameTime)
        {
            if(pairs != null && pairs.Count >0)
            foreach (Tuple<GameObject, GameObject> tuple in pairs)
            {
                if (tuple.Item1.isEnemyMovingFromSpawn)
                {
                    tuple.Item1.targetX = (int)Math.Round(tuple.Item2.moveObject.moveX);
                    tuple.Item1.targetY = (int)Math.Round(tuple.Item2.moveObject.moveZ);
                    tuple.Item1.moveObject.isThatFirstStep = true;
                    tuple.Item1.isEnemyMovingFromSpawn = false;

                }

                tuple.Item1.moveObject.Move((float)gameTime.ElapsedGameTime.TotalMilliseconds, effect, (int)Math.Round(tuple.Item2.moveObject.moveX), (int)Math.Round(tuple.Item2.moveObject.moveZ));
                tuple.Item1.moveObject.isThatFirstStep = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
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
                moveTuplesToEachOther(gameTime);
                renewMovingToBase();
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

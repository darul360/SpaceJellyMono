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
    public class MoveLocalEnemyToWarrior : GameComponent
    {
        Game1 game1;
        SoundEffect effect;
        float timer = 2;
        float timer2 = 40;
        const float TIMER = 2;
        const float TIMER2 = 40;
        bool block1 = false, block2 = false;
        List<Tuple<GameObject, GameObject>> pairs;
        List<Tuple<GameObject, Vector3>> location;
        bool isSetted = false;
        public MoveLocalEnemyToWarrior(Game1 game1) : base(game1)
        {
            this.game1 = game1;
            pairs = new List<Tuple<GameObject, GameObject>>();
            location = new List<Tuple<GameObject, Vector3>>();
        }

        public void findPairs()
        {
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "enemyLocal")
                {
                    foreach (GameObject go2 in game1.gameObjectsRepository.getRepo())
                    {
                        if (go2.GameTag == "warrior")
                        {
                            if (Vector3.Distance(go.transform.translation, go2.transform.translation) < 4.0f)
                            {
                                if (!isTupleAtList(go2))
                                    pairs.Add(new Tuple<GameObject, GameObject>(go, go2));
                            }
                        }
                    }
                }
            }
        }

        public void setLocations()
        {
            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "enemyLocal")
                {
                    Debug.WriteLine(go+" "+ go.transform.translation);
                    location.Add(new Tuple<GameObject, Vector3>(go, go.transform.translation));
                }
            }
            isSetted = true;
        }

        public bool isTupleAtList(GameObject gameObject)
        {
            foreach (Tuple<GameObject, GameObject> tuple in pairs)
            {
                
                if (gameObject == tuple.Item2) return true;
            }
            return false;
        }

        public void renewMovingToBase()
        {
            foreach (Tuple<GameObject, GameObject> tuple in pairs)
            {
                //if (tuple.Item1 == null) pairs.Remove(tuple);
                //if (tuple.Item2.GetHp() < 0)
                //{
                //    pairs.Remove(tuple);
                //    tuple.Item1.isEnemyMovingFromSpawn = true;
                //    tuple.Item1.targetX = 17;
                //    tuple.Item1.targetY = 15;
                //    tuple.Item1.isMoving = true;
                //    tuple.Item1.moveObject.isThatFirstStep = true;
                //}

                if(Vector3.Distance(tuple.Item2.transform.translation,new Vector3(95, 0, 5)) > 6)
                {
                    foreach (Tuple<GameObject, Vector3> tupleLocation in location)
                        if (tupleLocation.Item1 == tuple.Item1)
                        {
                            tuple.Item1.targetX = (int)tupleLocation.Item2.X;
                            tuple.Item1.targetY = (int)tupleLocation.Item2.Z;
                            tuple.Item1.isMoving = true;
                            tuple.Item1.moveObject.isThatFirstStep = true;
                        }
                            
                }
            }
        }

        public void moveTuplesToEachOther(GameTime gameTime)
        {
            if (pairs != null && pairs.Count > 0)
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
            if (timer < 0)
            {
                if (!isSetted) setLocations();
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
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.GameObjectComponents;
using SpaceJellyMONO.PathFinding;
using SpaceJellyMONO.Repositories;
using SpaceJellyMONO.UnitsFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public class MoveObject
    {
        private GameObject gameObject;
        private float moveZ, moveX;
        private Transform transform;
        private bool isGameObjectMovable;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        List<Vector2> route;
        float Velocity;
        private MouseState lastMouseState = new MouseState();
        public int i = 1;
        bool isFinalPointReached = true;
        bool collision = false;
        float timer = 1000f;
        const float TIMER = 1000;


        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.gameObject = modelLoader;
            //Debug.WriteLine(modelLoader);
            this.transform = modelLoader.transform;
            this.Velocity = velocity;
        }

        public void spreadObjects(float tempx,float tempy)
        {
                if (route == null && gameObject.transform.translation == new Vector3(tempx,0,tempy))
                {
                    gameObject.mainClass.basicFloorGenerate.updateGrid();
                    Random rand = new Random();
                    int value = rand.Next(-2, 2);
                    gameObject.moveObject.route = gameObject.mainClass.findPath.findPath((int)gameObject.transform.translation.X, (int)gameObject.transform.translation.Z, (int)tempx + value, (int)tempy + value);
                    gameObject.moveObject.i = 1;
                    gameObject.moveObject.isFinalPointReached = false;
                }
        }

       
    

     
        private void moveToPoint(float deltatime)
        {
            if (i == route.Count - 1 && Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.02f)
            {
                isFinalPointReached = true;
                transform.translation.X = route[i].X;
                transform.translation.Z = route[i].Y;
                route = null;
                gameObject.isMoving = false;
                gameObject.targetX = 0;
                gameObject.targetY = 0;
            }
            if (isFinalPointReached == false)
            {
                    if (Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.05f && i < route.Count - 1)
                    {
                        unlockCells();
                        i++;
                    }

                    Vector2 direction = route[i] - new Vector2(transform.Translation.X, transform.Translation.Z);
                    direction.Normalize();
                    moveX = transform.Translation.X;
                    moveZ = transform.Translation.Z;
                    moveX += direction.X * deltatime * 0.001f;
                    moveZ += direction.Y * deltatime * 0.001f;
                    gameObject.transform.translation.X = moveX;
                    gameObject.transform.translation.Z = moveZ;
            }
        }


        public void unlockCells()
        {
            for (int i = 0; i < gameObject.mainClass.gridW; i++)
            {
                for (int j = 0; j < gameObject.mainClass.gridH; j++)
                {
                    for (int k = 0; k < gameObject.mainClass.gameObjectsRepository.getRepo().Count; k++)
                    {
                        if (!PathCollidersRepository.cylinders[i, j].Intersect(gameObject.mainClass.gameObjectsRepository.getRepo()[k].collider))
                        {
                            gameObject.mainClass.findPath.unblockCell(i, j);
                        }
                    }
                }
            }
        }

        private void CheckCollisions(float deltatime)
        {
            float elapsed = deltatime;
            timer -= elapsed;
            foreach (GameObject temp in gameObject.mainClass.gameObjectsRepository.getRepo())
            {
                if (temp != gameObject)
                {
                    if (ProcessCollisions(temp))
                    {
                        collision = true;
                        //Debug.WriteLine(collision);
                        
                            if (gameObject.GetType() == typeof(Warrior) || gameObject.GetType() == typeof(Enemy))
                            if (timer < 0)
                            {
                                temp.TakeDmg(gameObject.GetDmg());
                            //Debug.WriteLine(temp.GetHp());
                            timer = TIMER;
                        }
                    }
                    else
                    {
                        collision = false;
                    }
                }
            }
        }

        public bool ProcessCollisions(GameObject modelLoader2)
        {
            return gameObject.collider.Intersect(modelLoader2.collider);
        }

        public void Move(float deltatime, SoundEffect effect,int targetX,int targetZ)
        {
            CheckCollisions(deltatime);
            if (isFinalPointReached)
            {
                gameObject.mainClass.basicFloorGenerate.updateGrid();
                route = gameObject.mainClass.findPath.findPath((int)transform.Translation.X, (int)transform.Translation.Z, targetX, targetZ);
                i = 1;
                isFinalPointReached = false;
            }
            if (route != null)
                moveToPoint(deltatime);
        }
    }
}
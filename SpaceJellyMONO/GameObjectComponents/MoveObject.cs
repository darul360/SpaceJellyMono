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
        public float moveZ, moveX;
        private Transform transform;
        private bool isGameObjectMovable;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        List<Vector2> route;
        float Velocity;
        private MouseState lastMouseState = new MouseState();
        public int i = 1;
        public bool isThatFirstStep = true;
        bool pointNotReached = true;
        float timer = 1000f;
        const float TIMER = 1000;
        float timer2 = 1;
        const float TIMER2 = 1;


        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.gameObject = modelLoader;
            //Debug.WriteLine(modelLoader);
            this.transform = modelLoader.transform;
            this.Velocity = velocity;
        }
     
        private void moveToPoint(float deltatime)
        {
            if (i == route.Count - 1 && Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.2f)
            {
                isThatFirstStep = true;
                transform.translation.X = route[i].X;
                transform.translation.Z = route[i].Y;
                route = null;
                gameObject.isMoving = false;
                if(!gameObject.isObjectSelected)
                gameObject.mainClass.selectedObjectsRepository.getRepo().Remove(gameObject);
            }
            if (isThatFirstStep == false)
            {
                if (Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.05f && i < route.Count-1)
                    {
                        unlockCells();
                        i++;
                    }
                float elapsed2 = deltatime/1000;
                timer2 -= elapsed2;
                if (timer2 < 0)
                {
                    unlockCells();
                    timer2 = TIMER2;
                }
                Vector2 direction = route[i] - new Vector2(transform.Translation.X, transform.Translation.Z);
                    direction.Normalize();
                    moveX = transform.Translation.X;
                    moveZ = transform.Translation.Z;
                    moveX += direction.X * deltatime * 0.005f;
                    moveZ += direction.Y * deltatime * 0.005f;
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

        public void CheckCollisions(float deltatime)
        {
            float elapsed = deltatime;
            timer -= elapsed;
            foreach (GameObject temp in gameObject.mainClass.gameObjectsRepository.getRepo())
            {
                if (temp != gameObject)
                {
                    if (ProcessCollisions(temp))
                    {

                        if (((gameObject.GetType() == typeof(Warrior) && temp.GetType() != typeof(Jelly)) || gameObject.GetType() == typeof(Enemy)) && gameObject.GetType() != temp.GetType())
                        {
                            gameObject.isFighting = true;
                            if (timer < 0)
                            {
                                temp.TakeDmg(gameObject.GetDmg());

                                //Debug.WriteLine(temp.GetHp());
                                timer = TIMER;
                            }
                        }
                        else
                        {
                            gameObject.isFighting = false;
                            temp.isFighting = false;
                        }
                    }
                    //else
                    //{
                    //    collision = false;
                    //}
                }
            }
        }

        public bool ProcessCollisions(GameObject modelLoader2)
        {
            return gameObject.collider.Intersect(modelLoader2.collider);
        }

        public void Move(float deltatime, SoundEffect effect,int targetX,int targetZ)
        {
            //CheckCollisions(deltatime);
            if (isThatFirstStep)
            {
                gameObject.mainClass.basicFloorGenerate.updateGrid();
                route = gameObject.mainClass.findPath.findPath((int)transform.Translation.X, (int)transform.Translation.Z, targetX, targetZ);
                if (route.Count == 1)
                {
                    route = null;
                    isThatFirstStep = true;
                    gameObject.isMoving = false;
                    if (!gameObject.isObjectSelected)
                        gameObject.mainClass.selectedObjectsRepository.getRepo().Remove(gameObject);
                }
                else
                isThatFirstStep = false;
                i = 1;
                
            }
            if (route != null)
                moveToPoint(deltatime);
        }
    }
}
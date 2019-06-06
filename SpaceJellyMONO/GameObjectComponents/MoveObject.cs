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
        private GameObject modelLoader;
        private float moveZ, moveX;
        private Transform transform;
        private bool isGameObjectMovable;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        List<Vector2> route;
        float Velocity;
        private MouseState lastMouseState = new MouseState();
        public int i = 1;
        bool isFinalPointReached;
        bool collision = false;
        float timer = 1000f;
        const float TIMER = 1000;


        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.modelLoader = modelLoader;
            //Debug.WriteLine(modelLoader);
            this.transform = modelLoader.transform;
            this.Velocity = velocity;
        }

        public int isPrimary()
        {
            int counter = 0;
            foreach (GameObject go in modelLoader.mainClass.gameObjectsRepository.getRepo())
            {
                if (go.isPrimary) counter++;
            }
            return counter;
        }

        //public GameObject findPrimary()
        //{
            //GameObject temporary = null;
            //if (isPrimary() == 0)
            //{
            //    float min = 100;
            //    foreach (GameObject go in modelLoader.mainClass.gameObjectsRepository.getRepo())
            //    {
            //        if (go.moveObject.route != null)
            //        {
            //            if (Vector3.Distance(go.transform.translation, new Vector3(go.moveObject.route[go.moveObject.route.Count - 1].X, 0, go.moveObject.route[go.moveObject.route.Count - 1].Y)) < min)
            //            {
            //                min = Vector3.Distance(go.transform.translation, new Vector3(go.moveObject.route[go.moveObject.route.Count - 1].X, 0, go.moveObject.route[go.moveObject.route.Count - 1].Y));
            //                temporary = go;
            //            }
            //        }
            //    }
            //    temporary.isPrimary = true;
            //}
            //return temporary;
       //}

        public void spreadObjects(float tempx,float tempy)
        {

                if (route == null && modelLoader.transform.translation == new Vector3(tempx,0,tempy))
                {

                    modelLoader.mainClass.basicFloorGenerate.updateGrid();
                    Random rand = new Random();
                    int value = rand.Next(-2, 2);
                    modelLoader.moveObject.route = modelLoader.mainClass.findPath.findPath((int)modelLoader.transform.translation.X, (int)modelLoader.transform.translation.Z, (int)((int)tempx + value), (int)((int)tempy + value));
                    modelLoader.moveObject.i = 1;
                    modelLoader.moveObject.isFinalPointReached = false;
                }
           

        }
    

     
        private void moveToPoint(float deltatime)
        {
            if (i == route.Count - 1 && Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.02f)
            {
                isFinalPointReached = true;
                transform.translation.X = route[i].X;
                transform.translation.Z = route[i].Y;
                float tempx,tempy;
                tempx = route[i].X;
                tempy = route[i].Y;
                route = null;
               //spreadObjects(tempx, tempy);
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
                    modelLoader.transform.translation.X = moveX;
                    modelLoader.transform.translation.Z = moveZ;
            }
        }


        public void unlockCells()
        {
            for (int i = 0; i < modelLoader.mainClass.gridW; i++)
            {
                for (int j = 0; j < modelLoader.mainClass.gridH; j++)
                {
                    for (int k = 0; k < modelLoader.mainClass.gameObjectsRepository.getRepo().Count; k++)
                    {
                        if (!PathCollidersRepository.cylinders[i, j].Intersect(modelLoader.mainClass.gameObjectsRepository.getRepo()[k].collider))
                        {
                            modelLoader.mainClass.findPath.unblockCell(i, j);
                        }
                    }
                }
            }
        }

        private void CheckCollisions(float deltatime)
        {
            float elapsed = deltatime;
            timer -= elapsed;
            foreach (GameObject temp in modelLoader.mainClass.gameObjectsRepository.getRepo())
            {
                if (temp != modelLoader)
                {
                    if (ProcessCollisions(temp))
                    {
                        collision = true;
                        //Debug.WriteLine(collision);
                        
                            if (modelLoader.GetType() == typeof(Warrior) || modelLoader.GetType() == typeof(Enemy))
                            if (timer < 0)
                            {
                                temp.TakeDmg(modelLoader.GetDmg());
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
            return modelLoader.collider.Intersect(modelLoader2.collider);
        }

        public void Move(float deltatime, SoundEffect effect)
        {
            CheckCollisions(deltatime);
            if (isGameObjectMovable)
            {
                if (modelLoader.isObjectSelected)
                {
                    MouseState currentState = Mouse.GetState();
                    if (currentState.RightButton == ButtonState.Pressed &&
                         lastMouseState.RightButton == ButtonState.Released)
                    {
                        modelLoader.mainClass.basicFloorGenerate.updateGrid();
                        lastClickedPos = modelLoader.mainClass.clickCooridantes.FindWhereClicked();
                        if (!modelLoader.mainClass.findPath.checkIfPositionIsBlocked((int)Math.Round(lastClickedPos.X), (int)Math.Round(lastClickedPos.Z)))
                        {
                            route = modelLoader.mainClass.findPath.findPath((int)transform.Translation.X, (int)transform.Translation.Z, (int)Math.Round(lastClickedPos.X), (int)Math.Round(lastClickedPos.Z));
                            i = 1;
                            isFinalPointReached = false;
                        }
                    }
                    lastMouseState = currentState;
                }
                if (route != null)
                    moveToPoint(deltatime);

            }
        }
    }
}
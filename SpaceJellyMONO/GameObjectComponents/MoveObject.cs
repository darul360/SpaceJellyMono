using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.PathFinding;
using SpaceJellyMONO.Repositories;
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
        int i = 1;
        bool isFinalPointReached;

        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.modelLoader = modelLoader;
            Debug.WriteLine(modelLoader);
            this.transform = modelLoader.transform;
            this.Velocity = velocity;
        }
        private void moveToPoint(float deltatime)
        {
            if (i == route.Count - 1 && Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.02f)
            {
                isFinalPointReached = true;
            }
            if (isFinalPointReached == false)
            {
                if (route.Count != 0)
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


        public  void unlockCells()
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


        public void Move(float deltatime, SoundEffect effect)
        {
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
                    if (route != null)
                    moveToPoint(deltatime);
                    lastMouseState = currentState;
                }
                if (isFinalPointReached == false && route !=null && transform.translation.X != route[route.Count-1].X && transform.translation.Z != route[i].Y)
                    moveToPoint(deltatime);
            }
        }
    }
}
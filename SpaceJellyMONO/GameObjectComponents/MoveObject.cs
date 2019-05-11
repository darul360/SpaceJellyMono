using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class MoveObject
    {
        private GameObject modelLoader;
        private float moveZ, moveX;
        private Transform transform;
        private bool isMovingActive, activate = false;
        private Vector2 direction;
        private Vector3 lastPosition;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        float Velocity;
        bool collision = false;

        public MoveObject(GameObject modelLoader, bool isMovingActive,float velocity)
        {
            this.isMovingActive = isMovingActive;
            this.modelLoader = modelLoader;
            this.transform = modelLoader.transform;
            this.moveX = this.transform.Translation.X;
            this.moveZ = this.transform.Translation.Z;
            this.Velocity = velocity;
        }

        private Vector3 FindWhereClicked()
        {
            GraphicsDevice graphicsDevice = modelLoader.mainClass.GraphicsDevice;
            MouseState mouseState = Mouse.GetState();

            Vector3 nearSource = new Vector3((float)mouseState.X, (float)mouseState.Y, 0f);
            Vector3 farSource = new Vector3((float)mouseState.X, (float)mouseState.Y, 1f);
            Vector3 nearPoint = graphicsDevice.Viewport.Unproject(nearSource, modelLoader.camera.Projection, modelLoader.camera.View, Matrix.Identity);
            Vector3 farPoint = graphicsDevice.Viewport.Unproject(farSource, modelLoader.camera.Projection, modelLoader.camera.View, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            Ray ray = new Ray(nearPoint, direction);

            Vector3 n = new Vector3(0f, 1f, 0f);
            Plane p = new Plane(n, 0f);

            float denominator = Vector3.Dot(p.Normal, ray.Direction);
            float numerator = Vector3.Dot(p.Normal, ray.Position) + p.D;
            float t = -(numerator / denominator);

            Vector3 pickedPosition = nearPoint + direction * t;

            return pickedPosition;
        }

        private void mover(float deltatime,Vector3 lp)
        {

            direction = new Vector2(lp.X, lp.Z) - new Vector2(transform.Translation.X, transform.Translation.Z);
            direction.Normalize();

            if (Math.Abs(lp.X-transform.Translation.X)>0.2f && Math.Abs(lp.Z-transform.Translation.Z)>0.2f)
            {
                    moveX += direction.X * deltatime * Velocity;
                    moveZ += direction.Y * deltatime * Velocity;
                    transform.Translation = new Vector3(moveX, transform.Translation.Y, moveZ);
            }
            else activate = false;
        }

        private void CheckCollisions()
        {
            foreach (GameObject temp in modelLoader.mainClass.gameObjectsRepository.getRepo())
            {
                if (temp != modelLoader)
                {
                    if (ProcessCollisions(temp))
                    {
                        collision = true;
                    }
                    else
                    {
                        collision = false;
                    }
                    //Debug.WriteLine(collision);
                }
            }
        }

        public void Move(float deltatime)
        {
            if (isMovingActive)
            {
                CheckCollisions();

                if (collision == false)
                {
                    lastPosition = new Vector3(moveX, transform.Translation.Y, moveZ);
                }
                    MouseState mouseState = Mouse.GetState();
                    if (modelLoader.isObjectSelected)
                    {
                        if (mouseState.RightButton == ButtonState.Pressed)
                        {
                            activate = true;
                            lastClickedPos = FindWhereClicked();
                        }
                    }

                    if (activate)
                    {
                        mover(deltatime, lastClickedPos);
                    }
                

                if (collision)
                {
                    transform.Translation = lastPosition;
                    CheckCollisions();
                }
            }
        }
        

        public bool ProcessCollisions(GameObject modelLoader2)
        {
            return modelLoader.collider.Intersect(modelLoader2.collider);
        }

    }
}

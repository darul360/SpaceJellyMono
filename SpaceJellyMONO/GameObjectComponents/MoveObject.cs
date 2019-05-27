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
        private bool isGameObjectMovable, active = false;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        List<Vector2> route;
        float Velocity;
        private MouseState lastMouseState = new MouseState();
        int i = 1;

        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.modelLoader = modelLoader;
            Debug.WriteLine(modelLoader);
            this.transform = modelLoader.transform;
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

        private void moveToPoint(float deltatime)
        {

             if (Vector2.Distance(new Vector2(transform.Translation.X, transform.Translation.Z), route[i]) <= 0.05f && i < route.Count -1)
                    {
                //transform.translation.X = route[i].X;
                //transform.translation.Z = route[i].Y;
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
                    Debug.WriteLine(i+ " "+direction + " " + modelLoader.transform.translation.X + " " + modelLoader.transform.translation.Z);
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
                        lastClickedPos = FindWhereClicked();
                        route = modelLoader.mainClass.findPath.findPath((int)transform.Translation.X, (int)transform.Translation.Z, (int)Math.Round(lastClickedPos.X), (int)Math.Round(lastClickedPos.Z));
                        i = 1;
                    }
                    if (route != null)
                    moveToPoint(deltatime);
                    lastMouseState = currentState;
                    Debug.WriteLine(FindWhereClicked().ToString());
                }
            }
        }
    }
}
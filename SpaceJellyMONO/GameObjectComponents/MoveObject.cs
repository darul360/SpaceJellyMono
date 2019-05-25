using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.PathFinding;
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
        private bool isGameObjectMovable;
        Vector3 lastClickedPos = new Vector3(0, 0, 0);
        List<Vector2> route;
        float Velocity;
        private MouseState lastMouseState = new MouseState();

        public MoveObject(GameObject modelLoader, bool isMovingActive, float velocity)
        {
            this.isGameObjectMovable = isMovingActive;
            this.modelLoader = modelLoader;
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

        private void mover(float deltatime)
        {
            Velocity = 0.005f;

            for (int i = 0; i < route.Count; i++)
            {
                Vector2 direction = new Vector2(route[i].X, route[i].Y) - new Vector2(transform.Translation.X, transform.Translation.Z);
                direction.Normalize();
                moveX = transform.Translation.X;
                moveZ = transform.Translation.Z;

                Vector2 UnitSpeed = direction * Velocity;

                float distance = Vector2.Distance(route[i], new Vector2(transform.Translation.X, transform.Translation.Z));
                do {

                    moveX += UnitSpeed.X * deltatime;
                    moveZ += UnitSpeed.Y * deltatime;
                    transform.Translation = new Vector3(moveX, 0, moveZ);
                } while (distance > 0.2f);

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
                        lastClickedPos = FindWhereClicked();
                        route = modelLoader.mainClass.findPath.findPath((int)transform.Translation.X, (int)transform.Translation.Z, (int)lastClickedPos.X, (int)lastClickedPos.Z);
                        mover(deltatime);
                    }
                    lastMouseState = currentState;
                }
            }
        }

    }
}
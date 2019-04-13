using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceJellyMONO
{
    public class Camera : GameComponent
    {
        private Vector3 cameraPos;
        private Vector3 cameraRot;
        private float cameraSpeed;
        private Vector3 cameraLookAt;
        private GraphicsDeviceManager gdm;

        public Vector3 Position
        {
            get{return cameraPos;}
            set
            {
                cameraPos = value;
                UpdateLookAt();
            }
        }

        public Vector3 Rotation
        {
            get { return cameraRot; }
            set
            {
                cameraRot = value;
                UpdateLookAt();
            }
        }
        public Matrix Projection
        {
            get;
            protected set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(cameraPos, cameraLookAt, Vector3.Up);
            }
        }

        public Camera(Game game,Vector3 position,Vector3 rotation,float speed, GraphicsDeviceManager graphicsDeviceManager) : base(game)
        {
            gdm = graphicsDeviceManager;
            cameraSpeed = speed;
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.GraphicsDevice.Viewport.AspectRatio,
                0.05f,1000.0f);
            
            MoveTo(position, rotation);
        }
        private void MoveTo(Vector3 position,Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
        private void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), Rotation);
        }

        private Vector3 PreviewMove(Vector3 vec)
        {
            Matrix rotate = Matrix.CreateRotationY(cameraRot.Y);
            Vector3 movement = new Vector3(vec.X, vec.Y, vec.Z);
            movement = Vector3.Transform(movement, rotate);
            return cameraPos + movement;
        }

        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(cameraRot.X) * Matrix.CreateRotationY(cameraRot.Y);
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            cameraLookAt = cameraPos + lookAtOffset;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 moveVec = Vector3.Zero;
            KeyboardState ks = Keyboard.GetState();
            MouseState current_mouse = Mouse.GetState();
            
            if (current_mouse.Y <50.0f)
            {
                moveVec.Z = 1;
            }

            if (current_mouse.Y> gdm.PreferredBackBufferHeight-50.0f)
            {
                Debug.WriteLine("działam");
                moveVec.Z = -1;
            }
            if (current_mouse.X < 50.0f)
            {
                moveVec.X = 1;
            }
            if (current_mouse.X>gdm.PreferredBackBufferWidth-50.0f)
            {
                moveVec.X = -1;
            }

            if(moveVec != Vector3.Zero)
            {
                moveVec.Normalize();
                moveVec *= dt * cameraSpeed;
                Move(moveVec);
            }
            if (ks.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }

            base.Update(gameTime);
        }
    }
}

using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceJellyMONO
{
    public class ModelLoader : GameComponent
    {
        private Model model;
        private String modelPath;
        private Camera camera;
        private Game1 mainClass;
        private float rotationAngle,scale;
        private Matrix worldMatrix;
        private Vector3 translation;
        private float moveZ = 8f,moveX = 10f;
        public ModelLoader(String path,Camera camera,Game1 game1,float YrotationAngle,float scale):base(game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
            this.rotationAngle = YrotationAngle;
            this.scale = scale;
            translation = new Vector3(moveX, 0, moveZ);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        private Matrix createTranslation()
        {
            return Matrix.CreateTranslation(translation);
        }

        private Matrix getWorld()
        {
            return Matrix.CreateScale(scale) * Matrix.CreateRotationY(3 * rotationAngle) * createTranslation();
        }

        public void draw()
        {
            //Debug.WriteLine(moveX + " " + moveZ);
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.NumPad8))
            {
                translation = new Vector3(moveX, 0, moveZ);
                moveZ += 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad2))
            {
                translation = new Vector3(moveX, 0, moveZ);
                moveZ -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad6))
            {
                translation = new Vector3(moveX, 0, moveZ);
                moveX -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad4))
            {
                translation = new Vector3(moveX, 0, moveZ);
                moveX += 0.2f;
            }

            model = mainClass.exportContentManager().Load<Model>(modelPath);
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach(BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = camera.View;
                    basicEffect.World = getWorld();
                    basicEffect.Projection = camera.Projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
            }

        }
    }
}

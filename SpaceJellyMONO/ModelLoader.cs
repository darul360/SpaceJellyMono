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
        private Vector3 translation;
        private String modelPath;
        private Camera camera;
        private Game1 mainClass;
        private float rotationAngle,scale;
        private Matrix worldMatrix;
        private float moveZ,moveX;

        public BoundingBox box;
        float size = 10;

        public ModelLoader(String path,Camera camera,Game1 game1,float YrotationAngle,float scale, Vector3 translation):base(game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
            this.rotationAngle = YrotationAngle;
            this.scale = scale;
            this.translation = translation;
            moveZ = translation.Z;
            moveX = translation.X;
            box = new BoundingBox(new Vector3(translation.X - size/2, translation.Y, translation.Z -size/2), new Vector3(translation.X + size / 2, translation.Y + size, translation.Z + size / 2));
        }


        public void update()
        {
            Debug.WriteLine(moveX + " " + moveZ);
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.NumPad8))
            {
                translation = new Vector3(moveX, 1, moveZ);
                moveZ += 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad2))
            {
                translation = new Vector3(moveX, 1, moveZ);
                moveZ -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad6))
            {
                translation = new Vector3(moveX, 1, moveZ);
                moveX -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad4))
            {
                translation = new Vector3(moveX, 1, moveZ);
                moveX += 0.2f;
            }
        }


        private Matrix createTranslation()
        {
            return Matrix.CreateTranslation(translation);
        }

        private Matrix getWorld()
        {
            return Matrix.CreateScale(scale) * Matrix.CreateRotationY(3 * rotationAngle) * createTranslation();
        }

        public bool processCollisions(BoundingBox otherBox, float DX, float DZ)
        {
            box = new BoundingBox();
            if (box.Intersects(otherBox))
            {
                return true;
            }
            else
                return false;
        }

        public void draw()
        {

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

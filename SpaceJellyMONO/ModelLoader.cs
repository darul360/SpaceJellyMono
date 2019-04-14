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
        private float moveZ,moveX;

        public BoundingBox box;
        float size = 7;

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


        public void update(BoundingBox referenceBox)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.NumPad8))
            {
                moveZ += 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad2))
            {
                //translation = new Vector3(moveX, 1, moveZ);
                moveZ -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad6))
            {
                //translation = new Vector3(moveX, 1, moveZ);
                moveX -= 0.2f;
            }

            if (ks.IsKeyDown(Keys.NumPad4))
            {
               // translation = new Vector3(moveX, 1, moveZ);
                moveX += 0.2f;
            }

            bool collisionX = processCollisions(referenceBox, moveX, moveZ);
            bool collisionZ = processCollisions(referenceBox, moveX, moveZ);
            if (collisionX)
            {
                Debug.WriteLine("collisionX"+moveX+" "+moveZ);
                moveX += 0;
            }
            if (collisionZ)
            {
                Debug.WriteLine("collisionY");
                moveZ += 0;
            }

            translation = new Vector3(moveX, 1, moveZ);
            

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
            box = new BoundingBox(new Vector3(translation.X - size / 2 + DX, translation.Y, translation.Z - size / 2 + DZ), new Vector3(translation.X + size / 2 + DX, translation.Y + size, translation.Z + size / 2 + DZ));
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

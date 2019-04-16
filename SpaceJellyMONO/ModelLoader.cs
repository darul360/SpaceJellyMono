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
        private DrawBoxCollider drawBoxCollider;
        private Vector3[] verticies = new Vector3[8];
        bool one,two,three,four;



        public BoundingBox box;
        float size = 0.8f;

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
            box = new BoundingBox(new Vector3(translation.X - size / 2, translation.Y, translation.Z - size / 2), new Vector3(translation.X + size / 2, translation.Y + size, translation.Z + size / 2));
            drawBoxCollider = new DrawBoxCollider(game1.GraphicsDevice, game1,"texture.jpg");
            one = false;
            two = false;
            three = false;
            four = false;
        }


        public void update(BoundingBox referenceBox)
        {
            Debug.WriteLine("1" + one + "2"+two+"3"+three+"4"+four);
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.NumPad8) && two == false )
            {
                moveZ += 0.01f;
            }

            if (ks.IsKeyDown(Keys.NumPad2) && one == false)
            {
                moveZ -= 0.01f;
            }

            if (ks.IsKeyDown(Keys.NumPad6) && four == false )
            {
                moveX -= 0.01f;
            }

            if (ks.IsKeyDown(Keys.NumPad4) && three == false)
            {
                moveX += 0.01f;
            }

            bool collisionX = processCollisions(referenceBox);
            bool collisionZ = processCollisions(referenceBox);

            Vector3[] tempBBLocation = new Vector3[8];
            tempBBLocation = referenceBox.GetCorners();


            #region ZCollisions
            if (frontWallCenter(verticies).Z < backWallCenter(tempBBLocation).Z+0.02f)
            {
                if (collisionZ) { two = true; collisionX = false; }
                if (!collisionZ) two = false;
            }

            if (backWallCenter(verticies).Z > frontWallCenter(tempBBLocation).Z - 0.02f)
            {
                if (collisionZ) { one = true; collisionX = false; }
                if (!collisionZ)  one = false;
            }
            #endregion
            Debug.WriteLine(rightWalllCenter(tempBBLocation).X + 0.02f + " " + leftWalllCenter(verticies).X);
            #region XCollisions
            if(rightWalllCenter(tempBBLocation).X + 0.02f > leftWalllCenter(verticies).X)
            {
                if (collisionX) { three = true; collisionZ = false; }
                if (!collisionX) three = false;
            }

            if(leftWalllCenter(tempBBLocation).X -0.02f < rightWalllCenter(verticies).X)
            {
                if(collisionX) { four = true; collisionZ = false; }
                if (!collisionX) four = false;
            }

            #endregion



            translation = new Vector3(moveX, 1, moveZ);
        }

        #region InputBoundingBoxCenter
        public Vector3 frontWallCenter(Vector3 [] boundingBoxVerticies)
        {
            return new Vector3((boundingBoxVerticies[1].X + boundingBoxVerticies[3].X) / 2, (boundingBoxVerticies[1].Y + boundingBoxVerticies[3].Y) / 2, (boundingBoxVerticies[1].Z + boundingBoxVerticies[3].Z) / 2);
        }

        public Vector3 backWallCenter(Vector3[] boundingBoxVerticies)
        {
            return new Vector3((boundingBoxVerticies[4].X + boundingBoxVerticies[6].X) / 2, (boundingBoxVerticies[4].Y + boundingBoxVerticies[6].Y) / 2, (boundingBoxVerticies[4].Z + boundingBoxVerticies[6].Z) / 2);
        }

        public Vector3 leftWalllCenter(Vector3[] boundingBoxVerticies)
        {
            return new Vector3((boundingBoxVerticies[1].X + boundingBoxVerticies[6].X) / 2, (boundingBoxVerticies[1].Y + boundingBoxVerticies[6].Y) / 2, (boundingBoxVerticies[1].Z + boundingBoxVerticies[6].Z) / 2);

        }

        public Vector3 rightWalllCenter(Vector3[] boundingBoxVerticies)
        {
            return new Vector3((boundingBoxVerticies[0].X + boundingBoxVerticies[7].X) / 2, (boundingBoxVerticies[0].Y + boundingBoxVerticies[7].Y) / 2, (boundingBoxVerticies[0].Z + boundingBoxVerticies[7].Z) / 2);

        }
        #endregion

        public bool processCollisions(BoundingBox otherBox)
        {
            box = new BoundingBox(new Vector3(translation.X - size / 2, translation.Y, translation.Z - size / 2), new Vector3(translation.X + size / 2, translation.Y + size, translation.Z + size / 2));
            if (box.Intersects(otherBox))
            {
                return true;
            }
            else
                return false;
        }



        public void draw()
        {
            verticies = box.GetCorners();

            model = mainClass.exportContentManager().Load<Model>(modelPath);
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach(BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = camera.View;
                    basicEffect.World = Matrix.CreateScale(scale) * Matrix.CreateRotationY(3 * rotationAngle) * Matrix.CreateTranslation(translation);  
                    basicEffect.Projection = camera.Projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
                drawBoxCollider.Draw(camera, verticies);
            }

        }
    }
}

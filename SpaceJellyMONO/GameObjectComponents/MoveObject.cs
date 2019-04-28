using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class MoveObject
    {
        private ModelLoader modelLoader;
        private float moveZ, moveX;
        private Transform transform;
        private Vector3[] tempBBLocation = new Vector3[8];
        private bool one, two, three, four;
        private bool isMovingActive;
        bool collision;


        public MoveObject(ModelLoader modelLoader, bool isMovingActive)
        {
            this.isMovingActive = isMovingActive;
            this.modelLoader = modelLoader;
            this.transform = modelLoader.transform;
            this.moveX = this.transform.Translation.X;
            this.moveZ = this.transform.Translation.Z;
            one = false; two = false; three = false; four = false;
        }

        public void Move()
        {
            if (isMovingActive)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.NumPad8) && two == false)
                {
                    moveZ += 0.01f;
                }

                if (ks.IsKeyDown(Keys.NumPad2) && one == false)
                {
                    moveZ -= 0.01f;
                }

                if (ks.IsKeyDown(Keys.NumPad6) && four == false)
                {
                    moveX -= 0.01f;
                }

                if (ks.IsKeyDown(Keys.NumPad4) && three == false)
                {
                    moveX += 0.01f;
                }

                
                     collision = ProcessCollisions(findClosest());
                     tempBBLocation = findClosest().collider.box.GetCorners();


                #region XCollisions
                if (rightWalllCenter(tempBBLocation).X + 0.02f > leftWalllCenter(modelLoader.collider.box.GetCorners()).X && two == false && one == false && four == false)
                {
                    if (collision) { three = true; one = false;two = false;four = false; }
                    if (!collision) three = false;
                }

                if (leftWalllCenter(tempBBLocation).X - 0.02f < rightWalllCenter(modelLoader.collider.box.GetCorners()).X && two == false && one == false && three == false)
                {
                    if (collision) { four = true; one = false; two = false; three = false; }
                    if (!collision) four = false;
                }

                #endregion

                #region ZCollisions
                if (frontWallCenter(modelLoader.collider.box.GetCorners()).Z < backWallCenter(tempBBLocation).Z + 0.02f && three == false && one == false && four == false)
                {
                    if (collision) { two = true; one = false; three = false; four = false; }
                    if (!collision) two = false;
                }

                if (backWallCenter(modelLoader.collider.box.GetCorners()).Z > frontWallCenter(tempBBLocation).Z -0.02f && three == false && two == false && four == false)
                {
                    if (collision) { one = true; three = false; two = false; four = false; }
                    if (!collision) one = false;
                }
                #endregion

                transform.Translation = new Vector3(moveX, transform.Translation.Y, moveZ);
            }
        }

        #region InputBoundingBoxCenter
        public Vector3 frontWallCenter(Vector3[] boundingBoxVerticies)
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


        public bool ProcessCollisions(ModelLoader modelLoader2)
        {
            if (modelLoader.collider.box.Intersects(modelLoader2.collider.box))
            {
                return true;
            }
            else
                return false;

        }

        public ModelLoader findClosest()
        {
            float minDist = 100.0f;
            int index = 0;

            for (int i=0; i < modelLoader.mainClass.gameObjectsRepository.getRepo().Count; i++)
            {
                if(modelLoader.mainClass.gameObjectsRepository.getRepo()[i] != modelLoader)
                {
                    if (Vector3.Distance(modelLoader.mainClass.gameObjectsRepository.getRepo()[i].transform.Translation, modelLoader.transform.Translation) < minDist)
                    {
                        minDist = Vector3.Distance(modelLoader.mainClass.gameObjectsRepository.getRepo()[i].transform.Translation, modelLoader.transform.Translation);
                        index = i;
                    }
                }
            }
            return modelLoader.mainClass.gameObjectsRepository.getRepo()[index];
        }

    }
}

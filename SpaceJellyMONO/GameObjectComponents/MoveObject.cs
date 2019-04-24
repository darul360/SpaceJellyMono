using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class MoveObject
    {
        private GameObject modelLoader;
        private float moveZ, moveX;
        private Transform transform;
        private Vector3[] tempBBLocation = new Vector3[8];
        private bool isMovingActive;
        bool collision;


        public MoveObject(GameObject modelLoader, bool isMovingActive)
        {
            this.isMovingActive = isMovingActive;
            this.modelLoader = modelLoader;
            this.transform = modelLoader.transform;
            this.moveX = this.transform.Translation.X;
            this.moveZ = this.transform.Translation.Z;
        }

        public void Move()
        {
            //Debug.WriteLine(one + " " + two + " " + three + " " + four+" col"+collision);
            if (isMovingActive)
            {
                Vector3 lastPosition = transform.Translation;
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.NumPad8))
                {
                    moveZ += 0.01f;
                    Debug.WriteLine(modelLoader.mainClass.gameObjectsRepository.getRepo().Count); 
                }

                if (ks.IsKeyDown(Keys.NumPad2))
                {
                    moveZ -= 0.01f;
                }

                if (ks.IsKeyDown(Keys.NumPad6))
                {
                    moveX -= 0.01f;
                }

                if (ks.IsKeyDown(Keys.NumPad4))
                {
                    moveX += 0.01f;
                }


                transform.Translation = new Vector3(moveX, transform.Translation.Y, moveZ);

                foreach(GameObject temp in modelLoader.mainClass.gameObjectsRepository.getRepo())
                {
                    if(ProcessCollisions(temp))
                    {
                        collision = true;
                    }
                    
                }

                if (collision)
                {
                    Debug.WriteLine("collision!");
                    transform.Translation = lastPosition;
                }
                
            }
        }



        public bool ProcessCollisions(GameObject modelLoader2)
        {
            return modelLoader.collider.Intersect(modelLoader2.collider);
        }

    }
}

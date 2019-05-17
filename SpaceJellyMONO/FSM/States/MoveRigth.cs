using Microsoft.Xna.Framework;
using System;

namespace SpaceJellyMONO.FSM.States
{
    public class MoveRigth : State
    {
        protected virtual float getXMultipler => 1.001f;

        public override void OnEnter()
        {
            Console.WriteLine("Enter");
        }

        public override void OnExit()
        {
            Console.WriteLine("Exit");
        }

        public override void OnUpdate(GameTime gameTime, GameObject gameObject)
        {

           // Vector3 oldTranslation = gameObject.transform.Translation;
            //gameObject.transform.Translation = new Vector3(oldTranslation.X * getXMultipler, oldTranslation.Y, oldTranslation.Z);



        }
    }
}

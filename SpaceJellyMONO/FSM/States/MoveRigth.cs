using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceJellyMONO.FSM.States
{
    public class MoveRigth : State
    {
        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate(GameTime gameTime, GameObject gameObject)
        {
            gameObject.moveObject();
        }
    }
}

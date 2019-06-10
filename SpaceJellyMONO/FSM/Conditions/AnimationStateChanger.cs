using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.FSM.Trans
{
    public class AnimationStateChanger
    {
        public bool ChangeState(GameObject gameObject)
        {
            if(gameObject.isMoving)
            {
                return false;
            }
            return true;
            //return !gameObject.isMoving;
        }
    }
}

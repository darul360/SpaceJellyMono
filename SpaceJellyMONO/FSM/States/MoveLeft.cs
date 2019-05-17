using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.FSM.States
{
    public class MoveLeft:MoveRigth
    {
        protected override float getXMultipler => 0.99f;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.FSM
{
    public class Transition
    {
        public State TargetState;

        public Func<GameObject, bool> ChangeStateCondtion;
    }
}

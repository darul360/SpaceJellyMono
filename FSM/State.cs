using System.Collections.Generic;

namespace FSM
{
    public abstract class State
    {
        public HashSet<Transition> transisions = new HashSet<Transition>();

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();

    }
}

using Microsoft.Xna.Framework;
using SpaceJellyMONO;
using System.Collections.Generic;

namespace SpaceJellyMONO.FSM
{
    public abstract class State
    {
        public HashSet<Transition> transisions = new HashSet<Transition>();

        public abstract void OnEnter();
        public abstract void OnUpdate(GameTime gameTime, GameObject gameObject);
        public abstract void OnExit();

    }
}

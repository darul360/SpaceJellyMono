using Microsoft.Xna.Framework;
using SpaceJellyMONO;
using System;

namespace SpaceJellyMONO.FSM
{
    public class FinateStateMachine
    {
        State currentState;

        public void Initialize(GameObject gameObject)
        {
            currentState?.OnEnter(gameObject);
        }

        public void Update(GameTime gameTime, GameObject gameObject)
        {
            currentState.OnUpdate(gameTime, gameObject);
            foreach (Transition transition in currentState.transisions)
            {
                if (transition.ChangeStateCondtion(gameObject))
                {
                    currentState.OnExit(gameObject);
                    currentState = transition.TargetState;
                    currentState.OnEnter(gameObject);
                    return;
                }
            }
        }

        internal void setStartState(State state)
        {
            currentState = state;
        }

    }
}

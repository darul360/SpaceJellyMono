using Microsoft.Xna.Framework;
using SpaceJellyMONO;

namespace SpaceJellyMONO.FSM
{
    public class FinateStateMachine
    {
        State currentState;

        public void Initialize()
        {
            currentState?.OnEnter();
        }

        public void Update(GameTime gameTime, GameObject gameObject)
        {
            currentState.OnUpdate(gameTime, gameObject);
            foreach (Transition transition in currentState.transisions)
            {
                if (transition.ChangeState())
                {
                    currentState = transition.TargetState;
                    currentState.OnExit();
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

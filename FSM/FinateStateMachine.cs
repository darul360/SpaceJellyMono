using Microsoft.Xna.Framework;

namespace FSM
{
    public class FinateStateMachine
    {
        State currentState;

        public void Initialize()
        {
            currentState?.OnEnter();
        }

        public void Update(GameTime gameTime)
        {
            currentState.OnUpdate();
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

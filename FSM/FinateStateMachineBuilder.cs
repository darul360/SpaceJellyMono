using System;
using System.Collections.Generic;

namespace FSM
{
    public class FinateStateMachineBuilder
    {
        HashSet<State> states = new HashSet<State>();
        State startState = null;

        public FinateStateMachineBuilder AddState(State state)
        {
            states.Add(state);
            return this;
        }

        public FinateStateMachineBuilder AddTransion(State startState, Transition transition)
        {
            if (!states.Contains(startState){
                throw new Exception("start sate must be on state list");
            }

            if (!states.Contains(transition.TargetState){
                throw new Exception("transition must be on state list");
            }
            if (startState is null)
            {
                this.startState = startState;
            }

            startState.transisions.Add(transition);
            return this;
        }

        public FinateStateMachine Build()
        {
            FinateStateMachine finateStateMachine = new FinateStateMachine();
            finateStateMachine.setStartState(startState);
            return finateStateMachine;
        }
    }
}

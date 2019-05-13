using System;
using System.Collections.Generic;

namespace SpaceJellyMONO.FSM
{
    public class FinateStateMachineBuilder
    {
        HashSet<State> states = new HashSet<State>();
        State startState = null;

        public FinateStateMachineBuilder AddState(State state)
        {
            states.Add(state);
            if (startState is null)
            {
                startState = state;
            }
            return this;
        }

        public FinateStateMachineBuilder AddTransion(State startState, State targetSate, Func<bool> condition)
        {
            if (!states.Contains(startState))
            {
                throw new Exception("start sate must be on state list");
            }

            if (!states.Contains(targetSate))
            {
                throw new Exception("transition must be on state list");
            }


            startState.transisions.Add(
                new Transition
                {
                    TargetState = targetSate,
                    ChangeStateCondtion = condition
                });
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

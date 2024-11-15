using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    IStateBehavior CurrentState { get; set; }

    object Context { get; set; }

    void SolveBehavior();
    
    public void ChangeState(IStateBehavior nextState)
    {
        CurrentState = nextState;
        CurrentState.OnEnter(Context);
    }
}
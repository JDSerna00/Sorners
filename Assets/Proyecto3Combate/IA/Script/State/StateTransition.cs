using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIStateMachines
{
    public delegate bool StateTransitionDelegate();
}

public class StateTransition
{
    private AIStateMachines.StateTransitionDelegate[] transitionConditions;

    private IStateBehavior from;
    private IStateBehavior to;
    
    public StateTransition(IStateBehavior from, IStateBehavior to, params AIStateMachines.StateTransitionDelegate[] transitionConditions)
    {
        this.from = from;
        this.to = to;
        this.transitionConditions = transitionConditions;
    }

    public bool ShouldTransition()
    {
        foreach (var transitionCondition in transitionConditions)
        {
            if (!transitionCondition.Invoke()) return false;
        }

        return true;
    }

    public IStateBehavior To => to;
}
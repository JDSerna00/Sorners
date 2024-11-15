using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviourState : IStateBehavior
{
    private PatrolCharacterAIContext context;
    
    public void OnEnter(object context)
    {
        this.context = (PatrolCharacterAIContext)context;
    }

    public void OnUpdate()
    {
        if (context.enemy == null) return;
        context.agent.destination = context.enemy.position;
    }
}

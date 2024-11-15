using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviourState : IStateBehavior
{
    private PatrolCharacterAIContext context;
    
    public void OnEnter(object context)
    {
        this.context = (PatrolCharacterAIContext)context;
    }

    public void OnUpdate()
    {
        if (context.patrolTarget == null)
        {
            context.searchTimer += Time.deltaTime;
            if (context.searchTimer >= context.currentSearchDelaySeconds)
            {
                Vector2 randomDirection = Random.insideUnitCircle * context.searchRadius;
                context.patrolTarget = context.agentTransform.position + Vector3.right * randomDirection.x +
                                       Vector3.forward * randomDirection.y;
                context.agent.destination = context.patrolTarget.Value;
                context.currentSearchDelaySeconds = Random.Range(context.searchDelaySecondsMin, context.searchDelaySecondsMax);
                context.searchTimer = 0;
            }
            return;
        }

        if (context.agent.remainingDistance < 4)
        {
            context.patrolTarget = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourState : IStateBehavior
{
    private PatrolCharacterAIContext context;
    
    public void OnEnter(object context)
    {
        this.context = (PatrolCharacterAIContext)context;
    }

    public void OnUpdate()
    {
        if (context.animator.GetFloat("ActiveAttack") < 0.2f)
        {
            context.attackTimer += Time.deltaTime;
            if (context.attackTimer >= context.currentAttackDelay)
            {
                context.animator.SetTrigger("Attack");
                context.attackTimer = 0;
                context.currentAttackDelay = Random.Range(context.attackDelayMin, context.attackDelayMax);
            }
        }
    }
}

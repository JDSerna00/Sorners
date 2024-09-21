using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerBasedOnValue : StateMachineBehaviour
{
    [SerializeField] private string valueParam;
    [SerializeField] private string targetTrigger;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat(valueParam) > 0.5)
        {
            animator.ResetTrigger(targetTrigger);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPicker : StateMachineBehaviour
{
    [SerializeField] private string inputParameter;
    [SerializeField] private string curveValue;
    [SerializeField] private string defaultValue;
    [SerializeField] private string outputParameter;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(outputParameter,Mathf.Lerp(animator.GetFloat(defaultValue), animator.GetFloat(curveValue), animator.GetFloat(inputParameter)));
    }
}

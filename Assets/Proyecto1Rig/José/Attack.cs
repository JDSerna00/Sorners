using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private Animator animator;

    private bool AttackActive()
    {
        return animator.GetFloat("ActiveAttack") > 0.5;
    }

    public void LightAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.ReadValueAsButton()) return;
        // if (AttackActive()) return;
        animator.SetTrigger("Attack");
        animator.SetBool("HeavyAttack", false);
    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        bool clicked = ctx.ReadValueAsButton();
        if (clicked)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("HeavyAttack", true);
            animator.SetFloat("Charging", 1);
        }
        else
        {
            animator.SetFloat("Charging", 0);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}

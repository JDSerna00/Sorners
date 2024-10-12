using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Animator))]

public class Ataque : MonoBehaviour
{

    private Animator anim;
    private int currentWeapon = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true); 

    }

    /*private bool AttackActive()
    {
        return anim.GetFloat("ActiveAttack") > 0.5f;
    }*/

    public void LightAttack(InputAction.CallbackContext ctx)
    {
        if (!this.gameObject.activeInHierarchy) 
        {
            return; 
        }
        
        if (!anim.GetBool("canAttack")) return;
        bool val = ctx.performed; 
        if(val)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
        }
    }

    public void OnAttackEnding()
    {
        anim.SetBool("canAttack", true);
        anim.SetBool("HeavyAttack", false);
    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        /*if (!this.gameObject.activeInHierarchy)
        {
            return;
        }*/

        if (!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if (val)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
            anim.SetBool("HeavyAttack", true); 
        }
    }

    public void ChangeWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            currentWeapon = (currentWeapon + 1) % 2;
        }
    }

    /*public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        bool clicked = ctx.ReadValueAsButton();
        if (AttackActive()) return;
        if (clicked)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("HeavyAttack", true);
            //anim.SetFloat("Charging", 1);
        }
        else
        {
            //anim.SetFloat("Charging", 0);
            //  state.UpdateStamina(-40);
        }
    }
    */
}

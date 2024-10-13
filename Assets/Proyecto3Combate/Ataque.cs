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
    public GameObject Sword; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("WeaponType", currentWeapon);
        UpdateWeaponVisibility();
    }

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
            if (currentWeapon == 0) // Cambiar de puños a espada
            {
                anim.SetTrigger("ChangeWeapon"); // Animación de sacar la espada
                currentWeapon = 1;
            }
            else // Cambiar de espada a puños
            {
                anim.SetTrigger("ChangeWeapon"); // Animación de guardar la espada
                currentWeapon = 0;
            }

            anim.SetInteger("WeaponType", currentWeapon);
        }
    }

    public void OnWeaponChangeComplete()
    {
        UpdateWeaponVisibility();
    }

    private void UpdateWeaponVisibility()
    {
        if (currentWeapon == 1) // Espada equipada
        {
            Sword.SetActive(true);
        }
        else // Espada guardada
        {
            Sword.SetActive(false);
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

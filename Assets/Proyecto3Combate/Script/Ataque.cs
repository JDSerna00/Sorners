using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]

public class Ataque : MonoBehaviour
{

    private Animator anim;
    private int currentWeapon = 0;
    [SerializeField] private GameObject Sword;
    [SerializeField] private WeaponDamager swordWeapon; 
    [SerializeField] private WeaponDamager punchWeapon; 
    private WeaponDamager currentDamager; 
    public Material swordMaterial; 
    public float dissolveDuration = 1.0f; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("WeaponType", currentWeapon);
        UpdateWeaponVisibility();
        currentDamager = punchWeapon; 
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
            if (currentWeapon == 0) // Cambiar de punch a espada
            {
                anim.SetTrigger("ChangeWeapon"); 
                currentWeapon = 1;
                StartCoroutine(ActivateDissolveEffect(true));
                
            }
            else // Cambiar de espada a pu�os
            {
                anim.SetTrigger("ChangeWeapon"); 
                currentWeapon = 0;
                StartCoroutine(ActivateDissolveEffect(false));
                currentDamager = punchWeapon; 
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
    public void ToggleDamageDetector(float motionValue)
    {
        currentDamager.Toggle(motionValue);
    }

    public void UnBugCollider(){
        swordWeapon.AntiBug_Collider();
        punchWeapon.AntiBug_Collider();
    }

    private IEnumerator ActivateDissolveEffect(bool appearing)
    {
        float startValue = appearing ? 1.0f : 0.0f;
        float endValue = appearing ? 0.0f : 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            float dissolveValue = Mathf.Lerp(startValue, endValue, elapsedTime / dissolveDuration);
            swordMaterial.SetFloat("_DissolveAmount", dissolveValue);
            yield return null;
        }
        if (appearing)
        {
            Sword.SetActive(true);
        }
        else
        {
            Sword.SetActive(false);
        }
    }
}

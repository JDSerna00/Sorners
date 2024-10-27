using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using InClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Animator))]

public class Ataque : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private Character character;
    private int currentWeapon = 0;
    public GameObject Sword;
    public Material swordMaterial; 
    public float dissolveDuration = 1.0f; 
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private float detectionRadius;
    public UnityEvent onLockTarget;
    public UnityEvent onUnlockTarget;

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
            if (currentWeapon == 0) // Cambiar de pu�os a espada
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

     public void Lock(InputAction.CallbackContext ctx)
    {
        if (!gameObject.scene.IsValid()) return;
        if (!ctx.performed) return;
        if (character.State.IsLocked)
        {
            character.State.LockedTarget = null;
            onUnlockTarget?.Invoke();
            return;
        }
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
        if (detectedColliders.Length == 0) return;
        int bestFocusedTarget = 0;
        var cameraManager = character.Player.GetComponent<PlayerCameraManager>();
        for (int i = 0; i < detectedColliders.Length; i++)
        {
            float focusScore = cameraManager.GetFocusScore(detectedColliders[i].transform);
            float currentBestScore = cameraManager.GetFocusScore(detectedColliders[bestFocusedTarget].transform);
            if (1 - focusScore < 1 - currentBestScore) bestFocusedTarget = i;
        }
        character.State.LockedTarget = detectedColliders[bestFocusedTarget].transform;
        onLockTarget?.Invoke();
    }

    
}

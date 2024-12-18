using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterState : MonoBehaviour
{
    //Estas variables iniciales deberian ser parte de un sistema de progresion
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegen = 10f;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float stamina;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        stamina = maxStamina;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        stamina += Time.deltaTime * staminaRegen;
        stamina = Mathf.Min(stamina, maxStamina);
    }

    public bool UpdateStamina(float staminaDelta)
    {
        if(GetComponent<RootMotionNavigation>() != null) return false;
        if (stamina >= Mathf.Abs(staminaDelta))
        {
            stamina += staminaDelta;
            return true;
        }
        
        return false;
    }

    public bool UpdateHealth(float healthDelta)
    {
        if (currentHealth >= healthDelta)
        {
            currentHealth += healthDelta;
            if(GetComponent<PlayerInput>() != null)
            {
                UIManager.Instance.UpdateHealth(currentHealth); 
            }
            return true;
        }

        return false;
    }

    public float Stamina => stamina;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
}

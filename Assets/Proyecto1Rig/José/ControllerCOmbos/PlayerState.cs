using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float maxStamina = 250f;
    [SerializeField] private float staminaRegen = 10f;
    [SerializeField] private float stamina;

    private void Awake()
    {
        stamina = maxStamina;
    }
    private void Update()
    {
        stamina += Time.deltaTime * staminaRegen;
        stamina = Mathf.Min(stamina, maxStamina);
    }

    public bool UpdateStamina(float staminaDelta)
    {   
        if(stamina >= Mathf.Abs(staminaDelta))
        {
            stamina += staminaDelta;
            return true;
        }
        return false;
    }

    public float Stamina => stamina;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {

    }

    public bool UpdateHealth(float healthDelta)
    {
        if (currentHealth >= healthDelta* - 1)
        {
            currentHealth += healthDelta;
            return true;
        }
        //Morir
        Debug.Log($"Character ({gameObject.name} is dead");
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeSlider; 
    [SerializeField] private CharacterState enemyState; 
    [SerializeField] private float lerpSpeed = 5f; 

    private float targetHealth; 

    private void Start()
    {
        if (enemyState != null)
        {
            healthSlider.maxValue = enemyState.MaxHealth;
            easeSlider.maxValue = enemyState.MaxHealth;

            targetHealth = enemyState.CurrentHealth;

            healthSlider.value = targetHealth;
            easeSlider.value = targetHealth;
        }
    }

    private void Update()
    {
        if (enemyState != null)
        {
            targetHealth = enemyState.CurrentHealth;
            healthSlider.value = targetHealth;

            if (Mathf.Abs(easeSlider.value - targetHealth) > 0.01f) // Evita bucles infinitos por diferencias mínimas
            {
                easeSlider.value = Mathf.Lerp(easeSlider.value, targetHealth, Time.deltaTime * lerpSpeed);
            }
        }
    }
}

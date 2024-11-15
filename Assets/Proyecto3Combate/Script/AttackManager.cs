using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private WeaponDamager weaponDamager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (GetComponent<PlayerState>().UpdateStamina(-25)) return;
        anim.SetTrigger("Attack");
    }

    public void ToggleDamageDetector(float motionValue)
    {
        weaponDamager.Toggle(motionValue);
    }
}

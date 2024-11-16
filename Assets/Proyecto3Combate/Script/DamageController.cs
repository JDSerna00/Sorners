using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class DamageController : MonoBehaviour, IDamageReceiver
{
    private Animator anim;

    [SerializeField] private int faction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        bool isAlive = GetComponent<CharacterState>().UpdateHealth(-payload.damage);
        //Debug.Log("is alive = " + isAlive);
        Vector3 damageDirection = transform.InverseTransformPoint(payload.position).normalized;
        SendMessage("UnBugCollider");
        if (isAlive)
        {
            if (Mathf.Abs(damageDirection.x) >= Mathf.Abs(damageDirection.z))
            {
                anim.SetFloat("DamageX", damageDirection.x * (float)payload.severity);
                anim.SetFloat("DamageY", 0);
            }
            else
            {
                anim.SetFloat("DamageX", 0);
                anim.SetFloat("DamageY", damageDirection.z * (float)payload.severity);
            }
            anim.SetTrigger("Damaged");
            if(GetComponent<PlayerInput>() != null)
                anim.SetBool("canAttack", true);
        }
        else
        {
            anim.SetTrigger("Die");
            if(TryGetComponent<PlayerInput>(out PlayerInput input)){
                UIManager.Instance.Lose();
                input.enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
            }
            //if(TryGetComponent<PlayerInput>(out PlayerInput myInput)) myInput.enabled = false;
            if(TryGetComponent<SimplePatrolAndAttackStateMachine>(out SimplePatrolAndAttackStateMachine mutantAI)){
                mutantAI.enabled = false;
                UIManager.Instance.UpdateEnemyCount();
                GetComponent<Collider>().enabled = false;
                SendMessage("UnBugCollider");
            }
        }
    }

    public int Faction => faction;
}


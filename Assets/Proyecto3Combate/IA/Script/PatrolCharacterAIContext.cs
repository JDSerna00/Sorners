using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class PatrolCharacterAIContext
{
    [HideInInspector] public Transform agentTransform;
    public float searchRadius;
    public float searchDelaySecondsMin;
    public float searchDelaySecondsMax;
    [HideInInspector] public float currentSearchDelaySeconds;
    [HideInInspector] public float searchTimer;
    [HideInInspector] public Vector3? patrolTarget;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform enemy;
    [HideInInspector] public Animator animator;
    public float attackDelayMin;
    public float attackDelayMax;
    [HideInInspector] public float currentAttackDelay;
    [HideInInspector] public float attackTimer;

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class RootMotionNavigation : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    [SerializeField]private VectorDampener smoothMotionVector;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void Update()
    {
        smoothMotionVector.Update();
        Vector3 navMeshDelta = agent.nextPosition - transform.position;
        navMeshDelta.y = 0;
        float deltaX = Vector3.Dot(transform.right, navMeshDelta);
        float deltaY = Vector3.Dot(transform.forward, navMeshDelta);
        smoothMotionVector.TargetValue = new Vector2(deltaX, deltaY);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            smoothMotionVector.TargetValue = Vector2.zero;
        }
        anim.SetFloat("X", smoothMotionVector.CurrentValue.x * 200);
        anim.SetFloat("Y", smoothMotionVector.CurrentValue.y * 200);
    }

    public void OnAnimatorMove()
    {
        Vector3 rootPosition = anim.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }
}
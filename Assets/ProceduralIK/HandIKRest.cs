using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandIKRest : MonoBehaviour
{

    [SerializeField] private Transform detectionReference;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask detectionLayers;
    [SerializeField] private Transform hand;
    [SerializeField] private AvatarIKGoal handGoal;
    [SerializeField] private DampenerFloat animationTransition;
    [SerializeField] private RaycastHit hit;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animationTransition.Update();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        Collider[] detectedColliders = Physics.OverlapSphere(detectionReference.position, detectionRadius, detectionLayers);
        if (detectedColliders.Length <= 0) return;
        Vector3 nearestSurfacePoint = detectedColliders[0].ClosestPoint(hand.position);

        foreach (Collider detectedCollider in detectedColliders)
        {
            Vector3 currentClosestPoint = detectedCollider.ClosestPoint(hand.position);
            float currentHandDistance = Vector3.SqrMagnitude(currentClosestPoint - hand.position);

            if (currentHandDistance < 0.01f)
            {
                nearestSurfacePoint = currentClosestPoint;
                break;
            }

            float distanceToSurfacePoint = (nearestSurfacePoint - hand.position).sqrMagnitude;
            if (currentHandDistance < distanceToSurfacePoint)
            {
                nearestSurfacePoint = currentClosestPoint;
            }
        }
        Vector3 rayDir = nearestSurfacePoint - detectionReference.position;
        Ray r = new Ray(detectionReference.position, rayDir);
        RaycastHit hit;
        bool hasSurface = Physics.Raycast(r, out hit, rayDir.magnitude * 1.05f, detectionLayers);
        animationTransition.TargetValue = hasSurface ? 1 : 0;
        anim.SetIKPositionWeight(handGoal, animationTransition.CurrentValue);
        anim.SetIKPosition(handGoal, hit.point);
        Vector3 rotationAxis = Vector3.Cross(transform.up, hit.normal);
        float rotationAngle = Vector3.Angle(transform.up, hit.normal);
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);

        anim.SetIKRotationWeight(handGoal, animationTransition.CurrentValue);
        anim.SetIKRotation(handGoal, rotation);
    }
}

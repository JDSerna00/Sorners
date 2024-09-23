using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIKRestBone : MonoBehaviour
{
    [SerializeField] private Transform detectionReference;
    [SerializeField] private Transform hand;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask detectionLayers;
    private void OnAnimatorIK(int layerIndex)
    {
        Collider[] detectedCollider = Physics.OverlapSphere(detectionReference.position, detectionRadius, detectionLayers);
        if (detectedCollider.Length <= 0) return;
        Vector3 nearestSurfacePoint = detectedCollider[0].ClosestPoint(hand.position);
        foreach (Collider detectedColliders in detectedCollider)
        {
            Vector3 currentClosesPoint = detectedColliders.ClosestPoint(hand.position);
            //1. en caso de que la suma este dentro del collider, se asume que la mano es el punto más cercano
            if (Vector3.Distance(currentClosesPoint, hand.position) < 0.01f)
            {
                nearestSurfacePoint = currentClosesPoint;
                break;
            }


        }
        Ray r = new Ray(detectionReference.position, nearestSurfacePoint);
        Vector3 rayDir = nearestSurfacePoint - detectionReference.position;
        RaycastHit hit;
        bool hasSurface = Physics.Raycast(r, out hit, rayDir.magnitude * (1 + 0.05f), detectionLayers);

    }

    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if (detectionReference == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(detectionReference.position, detectionRadius);
    }

    #endif


}

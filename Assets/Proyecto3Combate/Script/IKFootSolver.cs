using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : StateMachineBehaviour
{

    [SerializeField] LayerMask detectionLayers;
    [SerializeField] private float extraHeight = 0.07f;
    [SerializeField] private FloatDampener leftFootOffsetDampener;
    [SerializeField] private FloatDampener rightFootOffsetDampener;

    [SerializeField] private QuaternionDampener leftFootRotationDampener; 
    [SerializeField] private QuaternionDampener rightFootRotationDampener;


    void FindSurfaceFromFoot(Animator animator, AvatarIKGoal goal, out float surfaceOffset, out Vector3 footPosition, out Vector3 surfaceNormal)
    {
        if ((int)goal > 1) throw new ArgumentException(""); 
        Transform foot = animator.GetBoneTransform((HumanBodyBones)(int)goal + 5);
        Vector3 rayOrigin = foot.position + Vector3.up;
        Vector3 rayDirection = Vector3.down;
        bool detected = Physics.SphereCast(rayOrigin, 0.1f, rayDirection, out RaycastHit hit, 1 + extraHeight * 2, detectionLayers);
        surfaceOffset = Mathf.Max(0, (hit.point.y - foot.position.y) + extraHeight); 
        footPosition = foot.position;
        surfaceNormal = hit.normal; 

    }

    Quaternion SolveFootRotation(Animator animator, AvatarIKGoal goal, Vector3 surfaceNormal)
    {
        Transform foot = animator.GetBoneTransform((HumanBodyBones)(int)goal + 5);
        Vector3 axis = Vector3.Cross(Vector3.up, surfaceNormal);
        float angle = Vector3.Angle(Vector3.up, surfaceNormal);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        return animator.GetIKRotation(goal) * rotation; 

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        FindSurfaceFromFoot(animator, AvatarIKGoal.LeftFoot, out float leftFootOffset, out Vector3 leftFootPosition, out Vector3 leftFootNormal);
        FindSurfaceFromFoot(animator, AvatarIKGoal.RightFoot, out float rightFootOffset, out Vector3 rightFootPosition, out Vector3 rightFootNormal);

        Quaternion leftFootRotation = SolveFootRotation(animator, AvatarIKGoal.LeftFoot, leftFootNormal);
        Quaternion rightFootRotation = SolveFootRotation(animator, AvatarIKGoal.RightFoot, rightFootNormal);

        leftFootOffsetDampener.TargetValue = leftFootOffset;
        rightFootOffsetDampener.TargetValue = rightFootOffset;
        leftFootRotationDampener.TargetValue = leftFootRotation;
        rightFootRotationDampener.TargetValue = rightFootRotation; 

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition + Vector3.up * leftFootOffset);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPosition + Vector3.up * rightFootOffset);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);

    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = animator.rootPosition; 
    }


}

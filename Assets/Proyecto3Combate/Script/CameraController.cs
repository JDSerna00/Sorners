using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext; 

public class CameraController : MonoBehaviour
{
    [SerializeField] float LockDetectionRadius;
    [SerializeField] CinemachineVirtualCamera VirtualCam;
    [SerializeField] CinemachineFreeLook FreeLookCam;
    [SerializeField] LayerMask Layer;
    [SerializeField] bool isLocked = false;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        VirtualCam.gameObject.SetActive(false);
    }

    public void Lock(CallbackContext ctx){
        if(ctx.performed)
        {
            if (isLocked)
            {
                UnlockCamera();
                return;
            }

        Collider[] TargetsInRange = Physics.OverlapSphere(transform.position, LockDetectionRadius, Layer);
        Debug.Log(TargetsInRange);
        if (TargetsInRange.Length==0)
        {
            return;
        } 
        Transform BestTarget = TargetsInRange[0].transform;
        for(int i = 1; i<TargetsInRange.Length;i++)
            {
                float Distance = Vector3.Distance(transform.position, BestTarget.position);
                if(Distance > Vector3.Distance(TargetsInRange[i].transform.position, transform.position))
                {
                    BestTarget = TargetsInRange[i].transform;
                }
            }
        isLocked = true;
        anim.SetBool("Locked", true);
        VirtualCam.LookAt = BestTarget;
        VirtualCam.gameObject.SetActive(true);
        FreeLookCam.gameObject.SetActive(false);

        }
    }

    void UnlockCamera(){
        isLocked = false; 
        anim.SetBool("Locked", false);
        FreeLookCam.gameObject.SetActive(true);
        VirtualCam.gameObject.SetActive(false);
    }

    /*#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, LockDetectionRadius);
    }
    #endif  */
}

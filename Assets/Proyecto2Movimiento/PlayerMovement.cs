using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class PlayerMovement : MonoBehaviour
{
    int velXId;
    int velYId;
    private bool jumpDesired;
    private bool pressingJump;
    private Camera mainCamera; 
    [SerializeField] Animator animator;
    [SerializeField] VectorDampener vectorDampener = new VectorDampener(true); 
    [SerializeField] Transform cameraTransform; 

    public void Move(CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
         if(dir != Vector2.zero) 
         {
            animator.SetBool("IsMoving", true);
         }
        else 
        {
            animator.SetBool("IsMoving", false);
        } 
        vectorDampener.TargetValue = dir;
        
    }

    public void OnAnimatorMove()
    {
        float interpolator = Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)); 
        Vector3 forward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up, interpolator);
        Vector3 projected = Vector3.ProjectOnPlane(forward, transform.up);
        Quaternion rotation = Quaternion.LookRotation(projected, transform.up); 
        animator.rootRotation = Quaternion.Slerp(animator.rootRotation, rotation, vectorDampener.CurrentValue.magnitude);
        animator.ApplyBuiltinRootMotion();

    }

    public void OnJump(CallbackContext ctx)
    {
        if (animator.GetBool("isJumping")) return;
        bool jumping = ctx.performed;
        if (jumping == true)
        {
            animator.SetTrigger("Jump");
        }
    }

    public void ToggleSprint(CallbackContext ctx)
    {
        bool value = ctx.ReadValueAsButton();
        vectorDampener.Clamp = !value;
    }

    private void Awake()
    {
        velXId = Animator.StringToHash("X");
        velYId = Animator.StringToHash("Y");
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        vectorDampener.Update();
        Vector2 dir = vectorDampener.CurrentValue;
        animator.SetFloat(velXId, dir.x);
        animator.SetFloat(velYId, dir.y);
    }
}
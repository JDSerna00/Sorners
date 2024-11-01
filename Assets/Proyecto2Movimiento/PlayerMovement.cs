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

    public void Move(CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        vectorDampener.TargetValue = dir;

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * dir.y + cameraRight * dir.x).normalized;
        Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection);

        animator.SetFloat(velXId, localMoveDirection.x);
        animator.SetFloat(velYId, localMoveDirection.z);
        animator.SetBool("IsMoving", true);
        
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
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private VectorDampener _damper;
    [SerializeField] private Transform _camera;

    int xVelocityId;
    int yVelocityId;

    private void Awake()
    {
        xVelocityId = Animator.StringToHash("XVelocity");
        yVelocityId = Animator.StringToHash("YVelocity");
    }

    private void Update()
    {
        _damper.Update();
        _animator.SetFloat(yVelocityId, _damper.CurrentValue.y);
        _animator.SetFloat(xVelocityId, _damper.CurrentValue.x);
    }

    public void ToggleSprint(CallbackContext ctxt)
    {
        bool val = ctxt.ReadValueAsButton();
        _damper.Clamp = !val;
    }

    public void Move(CallbackContext ctxt)
    {
        Vector2 dir = ctxt.ReadValue<Vector2>();
        _damper.TargetValue = dir;
    }

    private void OnAnimatorMove()
    {
        float interpolator = Mathf.Abs(Vector3.Dot(_camera.forward, transform.up));
        Vector3 forward = Vector3.Lerp(_camera.forward,_camera.up,interpolator);
        Vector3 proyected = Vector3.ProjectOnPlane(forward, transform.up);
        Quaternion rotation = quaternion.LookRotation(proyected,transform.up);
        _animator.rootRotation = Quaternion.Slerp(_animator.rootRotation,rotation,_damper.CurrentValue.magnitude);
        _animator.ApplyBuiltinRootMotion();
    }
}

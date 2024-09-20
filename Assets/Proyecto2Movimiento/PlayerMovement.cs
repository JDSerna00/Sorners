using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
public class PlayerMovement : MonoBehaviour
{
    int velXId;
    int velYId;

    [SerializeField] Animator animator;
    [SerializeField] VectorDampener vectorDampener = new VectorDampener(true);

    public void Move(CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        vectorDampener.TargetValue = dir;   
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
    }

    private void Update()
    {
        vectorDampener.Update();
        Vector2 dir = vectorDampener.CurrentValue;
        animator.SetFloat(velXId, dir.x);
        animator.SetFloat (velYId, dir.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Charactermovement : MonoBehaviour
{
    int velXId;
    int velYId;

    [SerializeField] private Animator anim;
    [SerializeField] private Suavitel motionVector = new Suavitel(true);
    


    public void Move(CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        motionVector.TargetValue = direction;
 
    }

    public void ToggleSprint(CallbackContext ctx)
    {
        bool val = ctx.ReadValueAsButton();
        motionVector.Clamp = !val;
    }
    

    private void Awake()
    {
        velYId = Animator.StringToHash("VelY");
        velXId = Animator.StringToHash("VelX");
    }

#if UNITY_EDITOR

    private void OnValidate()
    {

    }

#endif

    // Update is called once per frame
    void Update()
    {
        motionVector.Update();

        Vector2 direction = motionVector.CurrentValue;
        anim.SetFloat(velXId, direction.x);
        anim.SetFloat(velYId, direction.y);
    }

}

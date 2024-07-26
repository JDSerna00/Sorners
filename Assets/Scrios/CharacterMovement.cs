using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour
{
    int velXId;
    int velYId;

#if UNITY_EDITOR

    private void OnValidate()
    {
        move(motiondebug);
    }

#endif

    [SerializeField] private Animator anim;
    [SerializeField] private Vector3 motiondebug;

    private void Awake()
    {
        velYId = Animator.StringToHash("VelYId");
        velXId = Animator.StringToHash("VelXId");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(Vector3 motionDirection)
    {
        anim.SetFloat("VelX", motionDirection.x);
        anim.SetFloat("VelY", motionDirection.y);
    }
}

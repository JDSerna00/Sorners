using System;
using UnityEngine;

[Serializable]

public class FloatDampener 
{
    public float CurrentValue { get; private set; }
    public float TargetValue { get; set; }
    [field:SerializeField] public float SmoothingTime { get; private set; }
    private float velocity; 

    public void Update()
    {
        CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref velocity, SmoothingTime);
    }
}

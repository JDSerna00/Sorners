using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public struct DampenerFloat
{
    public float CurrentValue { get; private set; }
    public float TargetValue { get; set; }

    private float velocity;
    [field: SerializeField] public float SmoothTime { get; set; }

    public void Update()
    {
        CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref velocity, SmoothTime);
    }
}

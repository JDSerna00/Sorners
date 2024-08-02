using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Suavitel
{
    private Vector2 currentValue;
    private Vector2 targetValue;
    private Vector2 velocity;
    [SerializeField] private float smoothTime;
    [SerializeField] private float clampMagniture;
    private bool clamp;

    public Suavitel(bool clamp)
    {
        currentValue = Vector2.zero;
        targetValue = Vector2.zero;
        velocity = Vector2.zero;
        smoothTime = 0;
        clampMagniture = 0;
        this.clamp = clamp;
    }

    public void Update()
    {
        currentValue = Vector2.SmoothDamp(currentValue, clamp ? Vector2.ClampMagnitude(targetValue, clampMagniture) : targetValue, ref velocity, smoothTime);

    }

    public Vector2 CurrentValue => currentValue;
    public Vector2 TargetValue { set => targetValue = value; }
    public bool Clamp { set => clamp = value; }
}

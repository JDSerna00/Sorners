using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    [SerializeField] float SkySpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SkySpeed);
    }
}
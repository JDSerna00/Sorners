using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Referencia al personaje
    [SerializeField] private float rotationSpeed = 5f; // Velocidad de rotación
    private Vector2 input; 

    public void Look(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        // Rotación en el eje Y (horizontal) alrededor del personaje
        playerTransform.Rotate(Vector3.up, input.x * rotationSpeed * Time.deltaTime);

        // Rotación en el eje X (vertical) de la cámara
        float rotationX = transform.localEulerAngles.x - input.y * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // Limitar la rotación vertical
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);
    }
}

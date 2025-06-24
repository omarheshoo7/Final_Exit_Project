using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurntableRotator : MonoBehaviour
{
    private Transform carTransform;
    public float rotationSpeed = 20f;
    private bool isRotating = false;

    public void SetTargetCar(GameObject car)
    {
        carTransform = car.transform;
    }

    void Update()
    {
        if (isRotating && carTransform != null)
        {
            carTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    public void SetRotation(bool value)
    {
        isRotating = value;
    }

    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 2;
    public Transform target;
    public float disFromTarget = 5;

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float height;
    public float fov = 2;       

    float yaw;

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(height, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = target.position + new Vector3(0, fov, 0) - transform.forward * disFromTarget;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float sensitivity = 2;
    public Transform target;
    public Rigidbody targetRB;
    public float disFromTarget = 5;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float height;
    public float fov = 2;       

    public static float yaw;

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(height, yaw), ref rotationSmoothVelocity, 0);

        transform.eulerAngles = currentRotation;

        transform.position = target.position + new Vector3(0, fov, 0) - transform.forward * disFromTarget;
    }
}

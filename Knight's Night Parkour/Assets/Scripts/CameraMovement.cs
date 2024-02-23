using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 2;
    public Transform target;
    public float disFromTarget = 5;
    Vector2 yawMinMax = new Vector2 (-90, 90);

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float height;
    public float fov = 2;       

    float yaw;

    // Update is called once per frame
    void LateUpdate()
    {
        float rotationSmoothTime = 0.12f;
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        //yaw = yaw % 720;
        yaw = Mathf.Repeat(yaw, 360f);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    yaw = Mathf.Clamp(yaw, target.eulerAngles.y - 85, target.eulerAngles.y + 85);
        //}


        //if ( Mathf.Abs(transform.eulerAngles.y - target.eulerAngles.y) >= 90 )
        //{
        //    yaw = Mathf.Clamp(yaw, -90, 90);
        //}

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(height, yaw), ref rotationSmoothVelocity, 0);

        transform.eulerAngles = currentRotation;

        transform.position = target.position + new Vector3(0, fov, 0) - transform.forward * disFromTarget;
    }
}

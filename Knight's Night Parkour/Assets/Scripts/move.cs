using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Transform flag;

    public Transform cam;

    public float moveSpeed = 7;
    public float jump = 10;
    float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.05f;
    float turnSmoothVelocity;
    public static Vector2 inputDir;

    public static bool running;
    bool grounded;

    Vector3 respawn;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        grounded = true;

        flag = GameObject.FindWithTag("Respawn").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            respawn = transform.position;
            print(respawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir = input.normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jumping", true);

            if (grounded)
            {
                rb.AddForce(Vector3.up * jump);
                grounded = false;
            }
        }

        if (inputDir != Vector2.zero)
        {            
            bool nintey = Mathf.Abs(transform.eulerAngles.y - cam.eulerAngles.y) > 90;
            running = true;

            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float currentRotation = transform.eulerAngles.y;

            float val = Mathf.Abs((currentRotation - targetRotation) % 90f); //dgetHorizontalAngleBetween(transform, cam)


            if ((/*(val < 5) || */ (val > 85)) || nintey)
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0);
                print(Mathf.Abs(transform.eulerAngles.y - cam.eulerAngles.y).ToString() + "            " + val.ToString());
            }
            else
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
        }
        running = false;


        float speed = moveSpeed * inputDir.magnitude;

        Vector3 velocity = transform.forward * speed;

        transform.position += (velocity * Time.deltaTime);

        float animationSpeedPercent = ((running) ? 1 : 1) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if(grounded)
        {
            animator.SetBool("jumping", false);
        }

        if (transform.position.y < -20)
        {
            Respawn(respawn);
        }
    }

    private float getHorizontalAngleBetween(Transform trans1, Transform cam)
    {
       Vector3 charFor = new Vector3(trans1.forward.x, 0f, trans1.forward.z) .normalized;   
       Vector3 camFor = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;

       return Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(charFor, camFor));
    }

    void Respawn(Vector3 respawn)
    {
        transform.position = respawn;
    }
}

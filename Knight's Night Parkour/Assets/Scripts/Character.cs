using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Transform flag;
    public Transform cam;

    public float moveSpeed = 7;
    public float jump = 475;
    float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.05f;
    float turnSmoothVelocity;
    public static Vector2 inputDir;

    bool running;
    bool grounded;

    float targetRotation;
    float currentRotation;

    public static Vector3 respawn;
    private bool smoothRotatingInProcess = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        grounded = true;

        jump = 475;
        
        flag = GameObject.FindWithTag("Respawn").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            moveSpeed = 7;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (other.gameObject.CompareTag("Respawn"))
        {
            respawn = transform.position;
            print(respawn);
        }

        if(interactable != null)
        {
            interactable.Interact(this.GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jumping", true);
            moveSpeed = 3;

            if (grounded)
            {
                rb.AddForce(Vector3.up * jump);
                grounded = false;
            }
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir = input.normalized;
        bool moving = inputDir != Vector2.zero;

        if (moving)
        {
            running = true;

            targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            currentRotation = transform.eulerAngles.y;

            if (shouldSharpRotate(targetRotation, currentRotation) && !smoothRotatingInProcess)
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0);
            }
            else
            {
                smoothRotatingInProcess = true;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
        }
        else smoothRotatingInProcess = false;

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

    private bool shouldSharpRotate(float targetRot,float currRot)
    {
        float diff = inrange((inrange(targetRot)- inrange(currRot)));

        if ((diff > 85) && (diff <95)) { return true; }
        if ((diff > 120) && (diff < 240)) { return true; }
        if ((diff > 265) && (diff < 275)) { return true; }
        return false;
    }

    private float inrange(float targetRot)
    {
       if (targetRot < 0) { return inrange(targetRot + 360f); }
       if (targetRot >360) { return inrange(targetRot - 360f); }
        return targetRot;
    }

    void Respawn(Vector3 respawn)
    {
        transform.position = respawn;
    }
}

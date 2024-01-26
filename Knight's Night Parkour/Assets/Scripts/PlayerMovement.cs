using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public Transform cam;

    public float jumpHeight = 7;
    public float moveSpeed = 7;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.05f;
    public float gravity = -9.8f;
    float turnSmoothVelocity;
    float velocityY;

    bool running;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jumping", true);

            if (controller.isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
            }
        }

        if (inputDir != Vector2.zero)
        {
            running = true;
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        running = false;
        float speed = moveSpeed * inputDir.magnitude;

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
            animator.SetBool("jumping", false);
        }

        float animationSpeedPercent = ((running) ? 1 : 1) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if (transform.position.y < -20)
        {
            controller.enabled = false;
            transform.position = new Vector3(0, 1, 0);
            controller.enabled = true;
        } 
    }

    
}

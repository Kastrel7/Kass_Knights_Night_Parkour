using System.Collections;
using System.Collections.Generic;
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

    bool running;
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
        Vector2 inputDir = input.normalized;

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
            running = true;

            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float currentRotation = Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg; 
            float currentRotation = transform.eulerAngles.y;

            float val = Mathf.Abs((currentRotation - targetRotation) % 90f);

            //print(string.Format("({0} - {1}) % 90  =  {2}", currentRotation, targetRotation, val));

            if ( (val < 5) || (val > 85) )
            {
                //print(cam.eulerAngles.y);
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
            //transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
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

    void Respawn(Vector3 respawn)
    {
        transform.position = respawn;
    }
}

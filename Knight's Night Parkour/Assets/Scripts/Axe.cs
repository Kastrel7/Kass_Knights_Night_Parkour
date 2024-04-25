using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    float starty;
    Vector3 start;

    public void Interact(Collider other)
    {
        other.GetComponent<Transform>().position = Character.respawn;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        starty = transform.position.y; 
        start = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < starty - 7)
        {
            rb.AddForce(new Vector3(0, 100, 0));
        }
        else if (transform.position.y > starty)
        {
            transform.position = start;
        }
    }
}

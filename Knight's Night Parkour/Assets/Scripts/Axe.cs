using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    float starty;
    Vector3 start;
    int retract = 7;

    public void Interact(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            character.Respawn();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        starty = transform.position.y; 
        start = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < starty - retract)
        {
            rb.AddForce(new Vector3(0, 100, 0));
        }
        else if (transform.position.y > starty)
        {
            transform.position = start;
        }
    }
}

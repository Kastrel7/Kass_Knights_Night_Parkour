using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{ 
    public Rigidbody rb;
    int killCannonAt = 400;
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
    }

    private void Update()
    {
        if (transform.position.z > killCannonAt)
        {
            Destroy(gameObject);
        }
    }

}

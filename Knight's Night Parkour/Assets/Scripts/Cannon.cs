using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{ 
    public Rigidbody rb;
    public void Interact(Collider other)
    {
        other.GetComponent<Transform>().position = Character.respawn;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.z > 500)
        {
            Destroy(gameObject);
        }
    }

}

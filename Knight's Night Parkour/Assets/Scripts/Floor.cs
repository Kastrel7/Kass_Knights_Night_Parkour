using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, IInteractable
{
    public Rigidbody axe;
    public void Interact(Collider other)
    {
        axe.AddForce(new Vector3(0, -50000, 0));
    }

    private void Update()
    {
        
    }
}

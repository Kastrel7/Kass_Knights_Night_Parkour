using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour, IInteractable
{
    public Rigidbody axe;

    public void Interact(Collider other)
    {
        axe.AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);
    }
}

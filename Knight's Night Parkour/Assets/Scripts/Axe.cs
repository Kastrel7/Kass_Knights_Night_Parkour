using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour, IInteractable
{
    public void Interact(Collider other)
    {
        other.GetComponent<Transform>().position = Character.respawn;
    }

    private void Update()
    {
        if (transform.position.y < 14)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        }
        else if (transform.position.y > 21)
        {
            transform.position = new Vector3(0, 20.85f, 234);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{
    public void Interact(Collider other)
    {
        other.GetComponent<Transform>().position = Character.respawn;
    }


}

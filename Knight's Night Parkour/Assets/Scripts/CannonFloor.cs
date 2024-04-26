using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonFloor : MonoBehaviour, IInteractable
{
    bool fire = false;
    public Rigidbody cannon;
    Rigidbody clone;
    Vector3 start;
    Vector3[] cannonSpawns = new Vector3[5];

    public void Interact(Collider other)
    {
        if (!fire)
        {
            StartCoroutine(Fire());
        }
    }

    private void Start()
    {
        start = cannon.position;

        for(int x = 0; x < cannonSpawns.Length; x++)
        {
            cannonSpawns[x] = GameObject.Find("Cannon" + (x+1).ToString()).transform.position;
        }
    }

    IEnumerator Fire()
    {
        fire = true;
        for (int x = 0; x < cannonSpawns.Length; x++)
        {
            clone = Instantiate(cannon, cannonSpawns[x], transform.rotation);
            clone.transform.parent = cannon.transform.parent;
            clone.transform.localScale = cannon.transform.localScale;
            clone.AddForce(new Vector3(0, 0, 200), ForceMode.Impulse);

            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(Fire()); 
    }
}

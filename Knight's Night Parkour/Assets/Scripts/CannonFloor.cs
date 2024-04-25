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
    Vector3[] cannonSpawns = {new Vector3(65.05037f, 25.71549f, 215.6056f),new Vector3(81.05037f, 25.71549f, 215.6056f), new Vector3(98.05037f, 25.71549f, 215.6056f), new Vector3(115.0504f, 25.71549f, 215.6056f), new Vector3(131.0504f, 25.71549f, 215.6056f) };

    public void Interact(Collider other)
    {
        StartCoroutine(Fire());
        
    }

    private void Start()
    {
        start = cannon.position;
    }

    IEnumerator Fire()
    {
        for (int x = 0; x < cannonSpawns.Length; x++)
        {
            clone = Instantiate(cannon, cannonSpawns[x], transform.rotation);
            clone.transform.parent = cannon.transform.parent;
            clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            clone.AddForce(new Vector3(0, 0, 200), ForceMode.Impulse);

            yield return new WaitForSeconds(1);

            Destroy(clone.gameObject);
        }
        StartCoroutine(Fire()); 
    }
}

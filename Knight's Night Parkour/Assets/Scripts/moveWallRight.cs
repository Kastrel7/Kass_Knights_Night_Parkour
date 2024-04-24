using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveWallRight : MonoBehaviour
{
    float start, fin;
    bool retracting;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        fin = transform.position.x - 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 7, start - fin) + fin, transform.position.y, transform.position.z);
    }
}

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
        //moveWall(start, fin);
        if (transform.position.x >= (start - 0.5f) && transform.position.x <= (start + 0.5f))
        {
            retracting = false;
        }
        if (transform.position.x >= (fin - 0.5f) && transform.position.x <= (fin + 0.5f))
        {
            retracting = true;
        }

        if (!retracting)
        {
            transform.position = transform.position - new Vector3(0.02f, 0, 0);
        }
        else if (retracting)
        {
            transform.position = transform.position + new Vector3(0.02f, 0, 0);
        }
    }

    private void moveWall(float start, float fin)
    {

    }
}

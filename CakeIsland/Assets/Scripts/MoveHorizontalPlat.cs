using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontalPlat : MonoBehaviour
{

    public float goingLeft = -1f;
    public float lowerBound = 7f;
    public float upperBound = 14f;
    public float speed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPosition = transform.localPosition.x;

        if (xPosition <= lowerBound)
        {
            goingLeft = 1f;
        }
        if (xPosition >= upperBound)
        {
            goingLeft = -1f;
        }

        xPosition += speed * goingLeft;
        transform.localPosition = new Vector3(xPosition, transform.localPosition.y, transform.localPosition.z);
    }
}

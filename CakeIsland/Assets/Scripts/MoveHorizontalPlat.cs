using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used to move platforms horizontally.
public class MoveHorizontalPlat : MonoBehaviour
{
    // variables.
    public float goingLeft = -1f;
    public float lowerBound = 7f;
    public float upperBound = 14f;
    public float speed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame. Method for the movement. 
    void FixedUpdate()
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

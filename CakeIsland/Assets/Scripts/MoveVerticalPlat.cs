using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVerticalPlat : MonoBehaviour
{
    public float goingDown = -1f;
    public float lowerBound = -6f;
    public float upperBound = 4f;
    public float speed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float yPosition = transform.localPosition.y;

        if (yPosition <= lowerBound)
        {
            goingDown = 1f;
        }
        if (yPosition >= upperBound)
        {
            goingDown = -1f;
        }

        yPosition += speed * goingDown;
        transform.localPosition = new Vector3(transform.localPosition.x, yPosition, transform.localPosition.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used to rotate some platforms in level 2. 
public class RotatePlatform : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Rotates the platforms. 
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, moveSpeed, Space.Self);
    }
}

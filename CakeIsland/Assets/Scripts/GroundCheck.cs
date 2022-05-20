using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit2D Hit2DRight;
    private RaycastHit2D Hit2DLeft;
    private Rigidbody2D rigidBody;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        GroundCheckMethod();
    }


    private void GroundCheckMethod()
    {
        Hit2DRight = Physics2D.Raycast(rayCastOrigin.position, Vector2.right, 2f, layerMask);
        Hit2DLeft = Physics2D.Raycast(rayCastOrigin.position, Vector2.left, 2f, layerMask);
        Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.blue);
        if (Hit2DRight != false || Hit2DLeft != false)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
            Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.red);
        }

    }
}

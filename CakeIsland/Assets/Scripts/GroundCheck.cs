using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used to check the angle to go up a slope. 
public class GroundCheck : MonoBehaviour
{
    // variables.
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit2D Hit2DRight;
    private RaycastHit2D Hit2DLeft;
    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheckMethod();
    }

    // Method used to check the angle to go up a slope.
    private void GroundCheckMethod()
    {
        // Raycast to the right if the player is facing the right side and a raycast to the left if the player is facing the left side.
        Hit2DRight = Physics2D.Raycast(rayCastOrigin.position, Vector2.right, 0.85f, layerMask);
        Hit2DLeft = Physics2D.Raycast(rayCastOrigin.position, Vector2.left, 0.85f, layerMask);
        Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.blue);
        // If the ground is there, it calculates the angle with arcsin taking the distance between the feet and the ground, and the raycast origin to the ground.
        if (Hit2DRight != false || Hit2DLeft != false)
        {
            float DISTANCE_FEET_TOP = Vector2.Distance(new Vector2(rayCastOrigin.position.x, rayCastOrigin.position.y),
                    new Vector2(playerFeet.position.x, playerFeet.position.y));
            float angleInDegrees = 0f;
            if (PlayerController.facingRight) {
                if (Hit2DRight != false)
                {
                    float DISTANCE_FEET_RAYCAST = Vector2.Distance(new Vector2(playerFeet.position.x, playerFeet.position.y),
                        new Vector2(Hit2DRight.point.x, Hit2DRight.point.y));
                    float angle = Mathf.Asin(DISTANCE_FEET_TOP / DISTANCE_FEET_RAYCAST);
                    angleInDegrees = (360 / (2 * Mathf.PI)) * angle;
                }
            }
            
            if (!PlayerController.facingRight)
            {
                if (Hit2DLeft != false)
                {
                    float DISTANCE_FEET_RAYCAST = Vector2.Distance(new Vector2(playerFeet.position.x, playerFeet.position.y),
                        new Vector2(Hit2DLeft.point.x, Hit2DLeft.point.y));
                    float angle = Mathf.Asin(DISTANCE_FEET_TOP / DISTANCE_FEET_RAYCAST);
                    angleInDegrees = (360 / (2 * Mathf.PI)) * angle;
                }
            }

            if (angleInDegrees >= 30f && angleInDegrees <= 45f)
            {
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = 0f;
                Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.red);
            }
        }

    }
}

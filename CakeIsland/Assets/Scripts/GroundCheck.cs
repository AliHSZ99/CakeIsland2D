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
        Hit2DRight = Physics2D.Raycast(rayCastOrigin.position, Vector2.right, 0.85f, layerMask);
        Hit2DLeft = Physics2D.Raycast(rayCastOrigin.position, Vector2.left, 0.85f, layerMask);
        Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.blue);
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
                    /*float angle = Vector2.Angle(new Vector2(playerFeet.position.x, playerFeet.position.y), new Vector2(playerFeet.position.x, Hit2DRight.distance));*/
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
                    /*float angle = Vector2.Angle(new Vector2(playerFeet.position.x, playerFeet.position.y), new Vector2(playerFeet.position.x, Hit2DRight.distance));*/
                }
            }


            Debug.Log("Angle is " + angleInDegrees);

            if (angleInDegrees >= 30f && angleInDegrees <= 45f)
            {
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = 0f;
                Debug.DrawRay(rayCastOrigin.position, Vector2.right, Color.red);
            }
        }

    }
}

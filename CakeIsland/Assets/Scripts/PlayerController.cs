using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //For the player movements
    public float movementSpeed = 1f;
    //Determines on how high the player can jump
    public float jumpingForce = 1f;
    public bool isJumping;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        // For moving the player left to right
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;

        // For jumping 
        
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rigidBody.AddForce(new Vector2(0, jumpingForce), ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //Checks if the player is touching the ground. This prevents the character to jump on the air
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Checks if the player is not touching the ground. This prevents the character to jump on the air
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Direction that the player is facing.
    public static bool facingRight = true;
    // The player's movement speed.
    public float movementSpeed = 1f;
    // The player's jumping height.
    public float jumpingForce = 1f;
    // The player's jump status.
    public bool isJumping;

    // The respawn point of the player.
    [SerializeField]
    public GameObject respawnPoint;

    // The bullet that the player fires.
    public GameObject bullet;

    // The location that the bullet is fired from.
    [SerializeField]
    GameObject firePoint;

    // The rigidbody of the player.
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");

        // For moving the player left to right
        if (movement != 0)
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
        }

        // Flip Character to the Right if D or right arrow is pressed.
        if (movement > 0 && !facingRight)
        {
            flip();
        }

        // Flip Character to the Left if A or left arrow is pressed.
        if(movement < 0 && facingRight)
        {
            flip();
        }

        // For jumping 

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rigidBody.AddForce(new Vector2(0, jumpingForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //Checks if the player is touching the ground. This prevents the character to jump on the air
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }       

        if (collision.gameObject.tag == "edge")
        {
            Debug.Log("Out of play area.");
            this.transform.position = respawnPoint.transform.position;
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

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    
    void shoot()
    {
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }
}
  
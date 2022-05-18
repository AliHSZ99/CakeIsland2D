using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Direction that the player is facing.
    public static bool facingRight = true;
    // The player's movement speed.
    public float movementSpeed = 5f;
    // The player's jumping height.
    public float jumpingForce = 6.5f;
    // The player's jump status.
    public bool isJumping;

    public bool canShoot;

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

    // To create the animation of the player.
    public Animator playerAnimator;

    // This boolean variable is for Dialogue 
    public static bool isCollected;

    // For the point system 
    public int points;
    public TMP_Text pointLabel;

    //Hello

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("Speed", Mathf.Abs(movement * movementSpeed));

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

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Debug.Log("Player can shoot!");
            playerAnimator.SetBool("IsShooting", true);
            shoot();
            StartCoroutine(stopShootingForm());

        }
        else if (!Input.GetButtonDown("Jump") && isJumping == false)
        {
            playerAnimator.SetBool("IsJumping", false);
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
            PlayerInfo.health--;

            // For the tutorial only. Resets the health when it reachers 0
            if (PlayerInfo.health <= 0)
            {
                PlayerInfo.health = 3;
            }

            Debug.Log("Out of play area.");
            this.transform.position = respawnPoint.transform.position;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = respawnPoint.transform.position;
        }

        if (collision.gameObject.tag == "star")
        {
            Object.Destroy(collision.gameObject);
            isCollected = true;
            points += 100;
            pointLabel.text = "Points: " + points;
        }

        if (collision.gameObject.tag == "door")
        {
            SceneManager.LoadScene("TutorialLevelEnd");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Checks if the player is not touching the ground. This prevents the character to jump on the air
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnimator.SetBool("IsJumping", true);
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
        playerAnimator.SetBool("IsShooting", true);
    }

    public IEnumerator stopShootingForm()
    {
        yield return new WaitForSeconds(0.2f);
        playerAnimator.SetBool("IsShooting", false);
    }
}
  
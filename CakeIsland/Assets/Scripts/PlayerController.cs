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
    public AudioSource[] audioSources;

    public static bool canShootBoss;

    public bool canShoot;

    // The respawn point of the player.
    [SerializeField]
    public static GameObject respawnPoint;

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

    public TMP_Text pointLabel;

    // For unlocked checkpoints
    private GameObject unlockCheckpoint2;
    private GameObject unlockCheckpoint3;
    public bool isCheckpoint;

    // This is the platform in level 2 that should start going up and down when the player hits the trigger. 
    public GameObject goUpPlatformLevel2;

    //Hello
    // variable to check which scene is loaded. 
    public static string previousScene;

    // FOR SHOOTING DELAY
    public float fireRate;
    public float nextShot;

    public GameObject bottomPlayer;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f; 
        nextShot = -1f;
        previousScene = SceneManager.GetActiveScene().name;
        respawnPoint = GameObject.FindGameObjectWithTag("StartCheckpoint");
        rigidBody = GetComponent<Rigidbody2D>();
        canShoot = true;
        facingRight = true;
        pointLabel.text = "Points: " + PlayerInfo.points;

        if (isCheckpoint && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("BossLevel") )
        {
            unlockCheckpoint2 = GameObject.FindGameObjectWithTag("UnlockCheckpoint2");
            unlockCheckpoint2.SetActive(false);
            unlockCheckpoint3 = GameObject.FindGameObjectWithTag("UnlockCheckpoint3");
            unlockCheckpoint3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(canShootBoss);*/
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

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rigidBody.AddForce(new Vector2(0, jumpingForce), ForceMode2D.Impulse);
            audioSources[1].Play();
            //isJumping = true;
        }

        if (Input.GetButtonDown("Fire1") && (canShoot || canShootBoss))
        {
            Debug.Log("Player can shoot!");
            shoot();
            StartCoroutine(stopShootingForm());
        }
    }

    private void FixedUpdate()
    {
        raycastJumping();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //Checks if the player is touching the ground. This prevents the character to jump on the air
        /*if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }      */ 

        if (collision.gameObject.tag == "edge")
        {
            PlayerInfo.health--;
            audioSources[2].Play();

            // This code gives a bug when the player dies. We use the code below 
            /* if (LevelEnd.Equals("Null") && PlayerInfo.health == 0)
             {
                 PlayerInfo.health = 3;
             }
             else
             {
                 checkPlayerStatus();
             }*/

            // If the scene is Tutorial, it just resets the health of the player.
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialLevel"))
            {
                if (PlayerInfo.health <= 0 )
                {
                    PlayerInfo.health = 3;
                }
            } else
            {
                checkPlayerStatus();
            }

            Debug.Log("Out of play area.");
            this.transform.position = respawnPoint.transform.position;
        }
        // Make player go back to the checkpoint they are at when they hit a spike 
        if (collision.gameObject.tag == "spike")
        {
            this.transform.position = respawnPoint.transform.position;
            PlayerInfo.health--;
            audioSources[2].Play();
            checkPlayerStatus();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            audioSources[2].Play();
            // If the scene is Tutorial, it just resets the health of the player.
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialLevel"))
            {
                PlayerInfo.health--;
                if (PlayerInfo.health <= 0)
                {
                    PlayerInfo.health = 3;
                }
                this.transform.position = respawnPoint.transform.position;
            }
            else
            {
                PlayerInfo.health--;
                this.transform.position = respawnPoint.transform.position;
                checkPlayerStatus();
            }
        }

        if (collision.gameObject.tag == "bullet")
        {
            audioSources[2].Play();
            this.transform.position = respawnPoint.transform.position;
            PlayerInfo.health--;
            checkPlayerStatus();
        }

        if (collision.gameObject.tag == "door")
        {
            PlayerInfo.health = 3;
            SceneManager.LoadScene("TutorialLevelEnd");
        }
        if (collision.gameObject.tag == "doorLevel1")
        {
            PlayerInfo.health = 3;
            SceneManager.LoadScene("Level1End");
        }
        if (collision.gameObject.tag == "doorLevel2")
        {
            PlayerInfo.health = 3;
            SceneManager.LoadScene("Level2End");
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
        if (Time.time > nextShot) {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            audioSources[0].Play();
            playerAnimator.SetBool("IsShooting", true);
            nextShot = Time.time + fireRate;
        }
    }

    public IEnumerator stopShootingForm()
    {
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("IsShooting", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint2")
        {
            audioSources[3].Play();
            respawnPoint = unlockCheckpoint2;
            Destroy(collision.gameObject);
            unlockCheckpoint2.SetActive(true);
        }

        if (collision.gameObject.tag == "Checkpoint3")
        {
            audioSources[3].Play();
            respawnPoint = unlockCheckpoint3;
            Destroy(collision.gameObject);
            unlockCheckpoint3.SetActive(true);
        }
        
        // To check if the platform in level 2 should go up. 
        if (collision.gameObject.tag == "MoveUpTrigger") {
            goUpPlatformLevel2.GetComponent<MoveVerticalPlat>().enabled = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            Object.Destroy(collision.gameObject);
            isCollected = true;
            PlayerInfo.points += 20;
            audioSources[4].Play();
            Debug.Log(PlayerInfo.points);
            pointLabel.text = "Points: " + PlayerInfo.points;
        }

        if (collision.gameObject.tag == "star")
        {
            Object.Destroy(collision.gameObject);
            isCollected = true;
            PlayerInfo.points += 40;
            audioSources[5].Play();
            Debug.Log(PlayerInfo.points);
            pointLabel.text = "Points: " + PlayerInfo.points;
        }
    }

    public void checkPlayerStatus()
    {
        if (PlayerInfo.health <= 0)
        {
            //Checks if there's enough points to revive
            if (PlayerInfo.points >= 100)
            {
                SceneManager.LoadScene("DeathScreen");
            }
            else
            {
                SceneManager.LoadScene("GameOverScreen");
                PlayerInfo.points = 0;
                PlayerInfo.health = 3;

            }
        }
    }
    public void raycastJumping()
    {

        if (Physics2D.Raycast(bottomPlayer.transform.position, Vector2.down, 1f, layerMask))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }
}
  
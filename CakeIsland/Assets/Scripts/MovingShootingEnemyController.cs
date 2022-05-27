using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used for the controller of the moving shooting enemy.
public class MovingShootingEnemyController : MonoBehaviour
{

    // Variables.
    Rigidbody2D rb;

    float movementSpeed = 5;

    const string LEFT = "left";
    const string RIGHT = "right";

    public static string facingDirection;

    Vector3 baseScale;

    [SerializeField]
    public GameObject respawnPoint;

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDistance;

    [SerializeField]
    float fireCastDistance;

    [SerializeField]
    float castDistance;

    [SerializeField]
    float fireDistance;

    [SerializeField]
    GameObject firePoint;

    public GameObject bullet;

    public float time;


    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// Update is called once per frame. Used for the facing direction and shoot bullets when the player is in sight.
    void FixedUpdate()
    {
        float vX = movementSpeed;

        if (facingDirection == LEFT)
        {
            vX = -movementSpeed;
        }

        rb.velocity = new Vector2(vX, rb.velocity.y);

        if (isHittingWall() || isNearEdge())
        {
            if (facingDirection == LEFT)
            {
                flip(RIGHT);
            }
            else
            {
                flip(LEFT);
            }
        }
        if (isPlayerInSight())
        {
            shoot();
        }
    }

    // Method used to have the enemy face the player (Image rotated horizontally).
    void flip(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = newDirection;
    }

    // Method used to check if the player is in sight of the enemy and in which exact position they are.
    bool isPlayerInSight()
    {
        bool val = false;

        float fireDistance = fireCastDistance;

        if (facingDirection == LEFT)
        {
            fireDistance = -fireCastDistance;
        }

        Vector3 targetPosition = firePoint.transform.position;
        targetPosition.x += fireDistance;

        Debug.DrawLine(firePoint.transform.position, targetPosition, Color.green);

        if (Physics2D.Linecast(firePoint.transform.position, targetPosition, 1 << LayerMask.NameToLayer("Player")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    // Method used to check if the moving shooting enemy is hitting a wall.
    bool isHittingWall()
    {
        bool val = false;

        float castDistance = baseCastDistance;

        if (facingDirection == LEFT)
        {
            castDistance = -baseCastDistance;
        }

        Vector3 targetPosition = castPos.position;
        targetPosition.x += castDistance;

        Debug.DrawLine(castPos.position, targetPosition, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPosition, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool isNearEdge()
    {
        bool val = true;

        float castDistance = baseCastDistance;

        Vector3 targetPosition = castPos.position;
        targetPosition.y -= castDistance;

        Debug.DrawLine(castPos.position, targetPosition, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPosition, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }

    // Method used to have the player respawn at the respawn point when they collide with the 
    // moving shooting enemy's bullet.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Touched Player");
            collision.gameObject.transform.position = respawnPoint.transform.position;
        }
    }

    // Method used to instantiate the bullet when the moving shooting enemy shoots.
    void shoot()
    {
        time += Time.deltaTime;
        if (time >= 0.5)
        {
            time = 0;
            Instantiate(bullet,firePoint.transform.position,firePoint.transform.rotation);
        }
    }
}

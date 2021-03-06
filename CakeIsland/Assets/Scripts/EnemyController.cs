using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used for the enemy that follows the player.
public class EnemyController : MonoBehaviour
{
    // Variables
    Rigidbody2D rb;
    
    float movementSpeed = 5;

    const string LEFT = "left";
    const string RIGHT = "right";

    public AudioSource enemyHitSound;
    public AudioSource enemyShootSound;

    string facingDirection;

    Vector3 baseScale;

    [SerializeField]
    public GameObject respawnPoint;

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDistance;

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

    // Update is called once per frame. Used for the movement, edge, and to face the player.
    private void FixedUpdate()
    {

        float vX = movementSpeed;

        if(facingDirection == LEFT)
        {
            vX = -movementSpeed;
        }

        rb.velocity = new Vector2(vX, rb.velocity.y);

        if (isHittingWall() || isNearEdge())
        {
            if(facingDirection == LEFT)
            {
                flip(RIGHT);
            }
            else
            {
                flip(LEFT);
            }
        }
        
    }

    // Method used to have the enemy face the player (Image rotated horizontally).
    void flip(string newDirection)
    {
        Vector3 newScale = baseScale;

        if(newDirection == LEFT)
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

    // Method used to check if the enemy is hitting a wall.
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

        if(Physics2D.Linecast(castPos.position, targetPosition,1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    // Method used to check if the enemy is near the edge of a platform.
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

    // TO BE REMOVED
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Touched Player");
           // collision.gameObject.transform.position = respawnPoint.transform.position;
        }
    }
}

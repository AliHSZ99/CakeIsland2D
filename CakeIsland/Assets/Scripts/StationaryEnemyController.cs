using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemyController : MonoBehaviour
{

    public Rigidbody2D rb;

    public GameObject bullet;
    public GameObject firePoint;
    public GameObject player;

    public float time;

    public float shootDelay;

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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
        shootDelay = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isPlayerInSight())
        {
            shoot();
        }

        if (transform.position.x < player.transform.position.x)
        {
            flip(RIGHT);
        }
        else
        {
            flip(LEFT);
        }
    }

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

    bool isPlayerInSight()
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

        if (Physics2D.Linecast(castPos.position, targetPosition, 1 << LayerMask.NameToLayer("Player")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    void shoot()
    {
        time += Time.deltaTime;
        if(time >= 0.5)
        {
            time = 0;
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }      
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryFlyingEnemyController : MonoBehaviour
{

    public GameObject player;
    public float range;

    public Rigidbody2D rb;

    public GameObject bullet;
    public GameObject firePoint;

    public float time;

    public float shootDelay;

    const string LEFT = "left";
    const string RIGHT = "right";
    public static string facingDirection;

    Vector3 baseScale;

    public GameObject respawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
        shootDelay = 2;
        range = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.x < player.transform.position.x)
        {
            flip(RIGHT);
        }
        else
        {
            flip(LEFT);
        }

        if(Vector2.Distance(gameObject.transform.position,player.transform.position) <= range)
        {
            shoot();
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

    void shoot()
    {
        time += Time.deltaTime;
        if (time >= shootDelay)
        {
            time = 0;
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }
    }
}

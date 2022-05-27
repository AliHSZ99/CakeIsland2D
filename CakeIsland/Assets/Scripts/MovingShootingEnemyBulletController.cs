using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used for the bullet of the moving shooting enemy.
public class MovingShootingEnemyBulletController : MonoBehaviour
{
    // Variables
    public float speed = 10f;
    private float bulletLife = 2f;

    [SerializeField]
    public Rigidbody2D rb;

    [SerializeField]
    public GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, bulletLife);

        string direction = MovingShootingEnemyController.facingDirection;

        if (direction.Equals("right"))
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method used to destroy the bullet when it collides with the player or ground.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}

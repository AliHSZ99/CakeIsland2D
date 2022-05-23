using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryFlyingEnemyBulletController : MonoBehaviour
{
    public float speed;
    private float bulletLife;

    public Rigidbody2D rb;

    public GameObject player;
    Vector2 playerDirection;

    public GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        speed = 50f;
        bulletLife = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerDirection = (player.transform.position - transform.position) * speed * Time.deltaTime;
        rb.velocity = new Vector2(playerDirection.x, playerDirection.y);
        Destroy(gameObject, bulletLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Shot.");
            /*collision.gameObject.transform.position = respawnPoint.transform.position;*/
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 10f;
    private float bulletLife = 1.5f;

    [SerializeField]
    public Rigidbody2D rb;

    [SerializeField]
    public GameObject respawnPoint;

    public AudioSource enemyShoot;

    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        enemyShoot = GameObject.Find("enemyTemp").GetComponent<EnemyController>().enemyShootSound;

        enemyShoot.Play();
        Destroy(gameObject, bulletLife);

        Debug.Log("ENEMY DIRECTION " + direction);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Shot.");
            /*collision.gameObject.transform.position = respawnPoint.transform.position;*/
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}

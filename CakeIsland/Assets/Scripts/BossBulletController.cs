using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float bulletLife;
    public Rigidbody2D rb;
    public AudioSource audioo;
    public AudioSource[] boss;
    public GameObject player;
    Vector2 playerDirection;


    // Start is called before the first frame update
    void Start()
    {
        speed = 400f;
        bulletLife = 7f;
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>().audioSources;
        playerDirection = (player.transform.position - transform.position) * speed * Time.deltaTime;
        rb.velocity = new Vector2(playerDirection.x, playerDirection.y);
        Destroy(gameObject, bulletLife);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerInfo.health == 1)
            {
                PlayerInfo.health = 3;
                BossController.PlayerDied();
            }
            Destroy(gameObject);
            PlayerInfo.health--;
            boss[4].Play();
        }

       
    }

}

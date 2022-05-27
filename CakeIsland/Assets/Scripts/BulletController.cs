using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script used for the bullet of the player.
public class BulletController : MonoBehaviour
{

    // Variables
    public float speed = 20f;
    private float bulletLife = 1f;

    DropPowerup dp;

    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public GameObject player;

    GameObject enemy;
    GameObject flyingEnemy;
    GameObject stationaryEnemy;
    AudioSource flyingEnemySound;
    AudioSource enemySound;
    AudioSource sound;
    AudioSource stationaryEnemySound;

    // Start is called before the first frame update
    void Start()
    {
        //Sounds for enemies
        if (SceneManager.GetActiveScene().name != "BossLevel" && SceneManager.GetActiveScene().name != "TutorialLevel") {
            stationaryEnemy = GameObject.Find("StationaryEnemyTemp");
            flyingEnemySound = GameObject.Find("FlyingEnemyTemp").GetComponent<FlyingEnemyController>().flyingEnemyHitSound;
            enemySound = GameObject.Find("enemyTemp").GetComponent<EnemyController>().enemyHitSound;
            stationaryEnemySound = stationaryEnemy.GetComponent<StationaryEnemyController>().stationaryEnemyHitSound;
        }

        if (SceneManager.GetActiveScene().name == "TutorialLevel") {
            flyingEnemySound = GameObject.Find("FlyingEnemyTemp").GetComponent<FlyingEnemyController>().flyingEnemyHitSound;
            enemySound = GameObject.Find("enemyTemp").GetComponent<EnemyController>().enemyHitSound;

        }

        

        dp = GetComponent<DropPowerup>();

        Destroy(gameObject, bulletLife);

        bool right = PlayerController.facingRight;

        if (right)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method used to have a death sound effect for each enemy type, 
    // destroy the bullet, and have a chance of a drop.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            string enemyName = collision.gameObject.name;
            if (enemyName.IndexOf("FlyingEnemy") != -1)  {
                flyingEnemySound.Play();
            } else if (enemyName.IndexOf("enemy") != -1) {
                enemySound.Play();
            } else if (enemyName.IndexOf("StationaryEnemy") != -1) {
                stationaryEnemySound.Play();
            }
            //Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
            dp.dropOrNot();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

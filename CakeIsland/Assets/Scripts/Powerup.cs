using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string type;
    public AudioSource[] playerSounds;

    // Start is called before the first frame update
    void Start()
    {

        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().audioSources;

        if(tag == "heart")
        {
            type = "heart";
        }
        /*else if(tag == "shoot")
        {
            type = "shoot";
        }*/
        else if(tag == "speed")
        {
            type = "speed";
        }
        else if(tag == "jump")
        {
            type = "jump";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Touched Collectible.");

            if (type == "heart")
            {
                playerSounds[6].Play();
                addLife(collision.gameObject);
            }

            /*if (type == "shoot")
            {
                shoot(collision.gameObject);
            }*/

            if (type == "speed")
            {
                playerSounds[8].Play();
                speedBoost(collision.gameObject);
            }

            if (type == "jump")
            {
                playerSounds[7].Play();
                jumpBoost(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }

    void addLife(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.addLife();
        Destroy(gameObject);
    }

    /*void shoot(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.shoot();
        Destroy(gameObject);
    }*/

    void speedBoost(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.speedBoost();
        Destroy(gameObject);
    }

    void jumpBoost(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.jumpBoost();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the powerups. 
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

    // Checks which powerup the player is touching. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == "heart")
            {
                playerSounds[6].Play();
                addLife(collision.gameObject);
            }

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

    // We add a life to the player if they touch a heart powerup.
    void addLife(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.addLife();
        Destroy(gameObject);
    }

    // We add a speed boost to the player if they touch a speed powerup.
    void speedBoost(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.speedBoost();
        Destroy(gameObject);
    }

    // We add a jump boost to the player if they touch a jump powerup.
    void jumpBoost(GameObject player)
    {
        PlayerInfo info = player.GetComponent<PlayerInfo>();
        info.jumpBoost();
        Destroy(gameObject);
    }
}

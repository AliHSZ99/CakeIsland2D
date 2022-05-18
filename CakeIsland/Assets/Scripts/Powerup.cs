using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string type;


    // Start is called before the first frame update
    void Start()
    {
        if(tag == "heart")
        {
            type = "heart";
        }
        else if(tag == "shoot")
        {
            type = "shoot";
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Touched Collectible.");

            if(type == "heart")
            {
                addLife(collision);
            }

            if(type == "shoot")
            {
                shoot(collision);
            }

            if(type == "speed")
            {
                speedBoost(collision);
            }

            if(type == "jump")
            {
                jumpBoost(collision);
            }
        }

        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }

    void addLife(Collision2D player)
    {
        PlayerInfo info = player.gameObject.GetComponent<PlayerInfo>();
        info.addLife();
        Destroy(gameObject);
    }

    void shoot(Collision2D player)
    {
        PlayerInfo info = player.gameObject.GetComponent<PlayerInfo>();
        info.shoot();
        Destroy(gameObject);
    }

    void speedBoost(Collision2D player)
    {
        PlayerInfo info = player.gameObject.GetComponent<PlayerInfo>();
        info.speedBoost();
        Destroy(gameObject);
    }

    void jumpBoost(Collision2D player)
    {
        PlayerInfo info = player.gameObject.GetComponent<PlayerInfo>();
        info.jumpBoost();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script used to have the boss randomly drop powerups/coins.
public class BossDropPowerUp : MonoBehaviour
{

    // Variables
    public int rand;
    public int dropThreshold, lifeThreshold, speedThreshold, jumpThreshold, coinThreshold;
    public GameObject lifePowerup, jumpPowerup, speedPowerup, coinPowerup;

    // Start is called before the first frame update
    void Start()
    {
        dropThreshold = 33;
        lifeThreshold = 20;
        coinThreshold = 40;
        speedThreshold = 60;
        jumpThreshold = 80;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method used to randomize the timing of powerup/coin drops.
    public void dropOrNot()
    {
        rand = Random.Range(0, 100);
        if (rand <= dropThreshold)
        {
            dropType();
        }
    }

    // Method used to drop a coin or specific powerup which the kind is chosen randomly.
    public void dropType()
    {
        //Debug.Log("Dropping item");
        rand = Random.Range(0, 100);
        if (rand <= lifeThreshold) // 20
        {
            dropLife();
        }
        else if (rand > lifeThreshold && rand <= coinThreshold) // 20 && 40
        {
            dropCoin();
        }
        else if (rand > coinThreshold && rand <= speedThreshold) // 40 && 60
        {
            dropSpeed();
        }
        else
        {
            dropJump();
        }
    }

    // Method used to instantiate the life powerup.
    public void dropLife()
    {
        Instantiate(lifePowerup, new Vector2(transform.position.x, (transform.position.y + 10)), transform.rotation);
    }

    // Method used to instantiate the speed powerup.
    public void dropSpeed()
    {
        Instantiate(speedPowerup, new Vector2(transform.position.x, (transform.position.y + 10)), transform.rotation);
    }

    // Method used to instantiate a coin.
    public void dropCoin()
    {
        Instantiate(coinPowerup, new Vector2(transform.position.x, (transform.position.y + 10)), transform.rotation);
    }

    // Method used to instantiate the jump powerup.
    public void dropJump()
    {
        Instantiate(jumpPowerup, new Vector2(transform.position.x, (transform.position.y + 10)), transform.rotation);
    }
}

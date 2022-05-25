using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropPowerup : MonoBehaviour
{
    public int rand;
    public int dropThreshold, lifeThreshold, speedThreshold, jumpThreshold, coinThreshold;
    public GameObject lifePowerup, coinPowerup, jumpPowerup, speedPowerup;

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

    public void dropOrNot()
    {
        rand = Random.Range(0,100);
        if (rand <= dropThreshold)
        {
            dropType();
        }
    }

    public void dropType()
    {
        Debug.Log("Dropping item");
        rand = Random.Range(0, 100);
        if(rand <= lifeThreshold) // 20
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

    public void dropLife()
    {
        Instantiate(lifePowerup, transform.position, transform.rotation);
    }

    public void dropCoin()
    {
        Instantiate(coinPowerup, transform.position, transform.rotation);
    }

    public void dropSpeed()
    {
        Instantiate(speedPowerup, transform.position, transform.rotation);
    }

    public void dropJump()
    {
        Instantiate(jumpPowerup, transform.position, transform.rotation);
    }
}

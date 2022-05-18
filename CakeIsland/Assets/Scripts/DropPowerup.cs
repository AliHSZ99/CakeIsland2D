using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerup : MonoBehaviour
{
    public int rand;
    public int dropThreshold, lifeThreshold, shootThreshold, speedThreshold, jumpThreshold;
    public GameObject lifePowerup, shootPowerup, jumpPowerup, speedPowerup;

    // Start is called before the first frame update
    void Start()
    {
        dropThreshold = 33;
        lifeThreshold = 20;
        shootThreshold = 40;
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
        rand = Random.Range(0, 100);
        if(rand <= lifeThreshold)
        {
            dropLife();
        }
        else if (rand > lifeThreshold && rand <= shootThreshold)
        {
            dropShoot();
        }
        else if (rand > shootThreshold && rand <= speedThreshold)
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
        Instantiate(lifePowerup,transform.position,transform.rotation);
    }

    public void dropShoot()
    {
        Instantiate(shootPowerup, transform.position, transform.rotation);
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

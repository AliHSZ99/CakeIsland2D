using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerup : MonoBehaviour
{
    public int rand;
    public int dropThreshold, lifeThreshold, /*shootThreshold,*/ speedThreshold, jumpThreshold;
    public GameObject lifePowerup, shootPowerup, jumpPowerup, speedPowerup;

    // Start is called before the first frame update
    void Start()
    {
        dropThreshold = 33;
        lifeThreshold = 33;
        /*shootThreshold = 40;*/
        speedThreshold = 66;
        jumpThreshold = 100;
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
        if(rand <= lifeThreshold)
        {
            dropLife();
        }
        /*else if (rand > lifeThreshold && rand <= shootThreshold)
        {
            dropShoot();
        }*/
        else if (rand > lifeThreshold && rand <= speedThreshold)
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

    /*public void dropShoot()
    {
        Instantiate(shootPowerup, transform.position, transform.rotation);
    }*/

    public void dropSpeed()
    {
        Instantiate(speedPowerup, transform.position, transform.rotation);
    }

    public void dropJump()
    {
        Instantiate(jumpPowerup, transform.position, transform.rotation);
    }
}

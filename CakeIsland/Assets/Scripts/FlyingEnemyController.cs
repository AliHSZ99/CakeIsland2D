using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used for the flying enemies that follow the player. 
public class FlyingEnemyController : MonoBehaviour
{
    // variables.
    public float range;
    public float speed;
    public GameObject player;
    public Vector2 startingSpot;
    public AudioSource flyingEnemyHitSound;
    public AudioSource flyingEnemyShootSound;

    // Start is called before the first frame update
    void Start()
    {
        startingSpot = transform.position;
        range = 10f;
        speed = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame. Moves towards the player. 
    void Update()
    {
        if(Vector2.Distance(gameObject.transform.position,player.transform.position) <= range)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startingSpot, speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerNew : MonoBehaviour
{

    public bool patrolling;

    // The rigidbody of the enemy.
    public Rigidbody2D rb;

    // The direction that the enemy is facing.
    public static bool facingRight = true;

    // The movement speed of the enemy.
    public float movementSpeed;

    // The range that the enemy can detect the player.
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        patrolling = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void patrol()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

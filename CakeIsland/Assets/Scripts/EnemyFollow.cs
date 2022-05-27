using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used to make the enemy follow the player
public class EnemyFollow : MonoBehaviour
{

    // Variables
    public Transform playerTransform;
    public Rigidbody2D rb;
    public GameObject player;
    public float speed;
    public bool facingRight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(playerTransform.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            
        }
        else
        {
            rb.MovePosition(newPos);
           // transform.LookAt(target);
        }
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }

    // Method used to have the enemy face the player (Image rotated horizontally).
    void Flip()
    {
        //here your flip function, as example
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 20f;
    private float bulletLife = 1.5f;

    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, bulletLife);

        bool right = PlayerController.facingRight;

        if (right)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
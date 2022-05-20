using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{

    public float time;
    public GameObject bullet;
    public GameObject firePoint;
    public GameObject player;
    public float shootingDelay;
    public float meleeDelay;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        time = 0;
        shootingDelay = 2f;
        meleeDelay = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            Melee();
        } else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        time += Time.deltaTime;
        if (time >= shootingDelay)
        {
            time = 0;
            animator.SetBool("damage", true);
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            //Instantiate(bullet, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y + 3), firePoint.transform.rotation);
            //Instantiate(bullet, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y - 3), firePoint.transform.rotation);

        }
    }

    void Melee()
    {
        time += Time.deltaTime;
        if (time >= meleeDelay)
        {
            time = 0;

            if (PlayerInfo.health == 1)
            {
                /*time += Time.deltaTime;
                time = 0;
                animator.SetBool("attack02", true);
                PlayerDied();*/
                StartCoroutine(ExecuteAfterTime(0.5f));

            }

            animator.SetBool("attack02", true);
            PlayerInfo.health--;
        }
    }

    void PlayerDied()
    {
            SceneManager.LoadScene("GameOverScreen");
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerDied();
    }

}

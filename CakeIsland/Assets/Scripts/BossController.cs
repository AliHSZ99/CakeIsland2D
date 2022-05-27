using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script used for the boss controller.
public class BossController : MonoBehaviour
{

    // Variables
    public float time;
    public GameObject bullet;
    public GameObject firePoint;
    public GameObject player;
    public float shootingDelay;
    public float powerUpDelay;
    public BossDropPowerUp dp;
    public float meleeDelay;
    public Animator animator;
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        time = 0;
        shootingDelay = 2f;
        meleeDelay = 2f;
        powerUpDelay = 2f;
        dp = GetComponent<BossDropPowerUp>();
        PlayerController.canShootBoss = true;
    }

    // Update is called once per frame
    void Update()
    {
        DroppingPowerUps();
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            Melee();
        } else
        {
            Shoot();
        }
    }

    // Method used for the two phases of shooting the boss will have during the boss level.
    void Shoot()
    {
        time += Time.deltaTime;
        if (time >= shootingDelay)
        {
            time = 0;
            if (BossInfo.health <= 5 && BossInfo.health != 0)
            {
                Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                Instantiate(bullet, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y + 3), firePoint.transform.rotation);
                Instantiate(bullet, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y - 3), firePoint.transform.rotation);
                animator.SetBool("attack", true);
                audioSources[2].Play();
                
            } else if (BossInfo.health > 5)
            {
                Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                animator.SetBool("damage", true);
                audioSources[1].Play();
            } 
        }
    }

    // Method used to have the boss do a melee attack if the player is too close to it.
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
            audioSources[0].Play();
            audioSources[4].Play();
        }
    }

    // Method used to put the player in either the death screen or game over screen depending on the amount of 
    // points they have when they die. 
    public static void PlayerDied()
    {
        if(PlayerInfo.points >= 100)
        {
            PlayerInfo.health = 3;
            BossInfo.health = 3;
            SceneManager.LoadScene("DeathScreen");
        }
        else
        {
            PlayerController.canShootBoss = false;
            PlayerInfo.health = 3;
            BossInfo.health = 3;
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    // Method used to put the player in the WinScreen scene whenever they beat the boss.
    public static void BossDied()
    {
        PlayerInfo.health = 3;
        PlayerController.canShootBoss = false;
        PlayerInfo.points = 0;
        SceneManager.LoadScene("WinScreen");
    }

    // Method used to have the player visibly see the boss bullet or melee attack hit them before they die.
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerDied();
    }

    // Method used to have the boss randomly drop a powerup.
    void DroppingPowerUps()
    {
        time += Time.deltaTime;
        if (time >= powerUpDelay)
        {
            time = 0;
            dp.dropOrNot();

        }
    }

    // Method used to reduce the health of the boss when hit by a player bullet and eventually kill it
    // when it doesn't have anymore health.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            if (BossInfo.health == 1)
            {
                BossDied();
            }
            Destroy(collision.gameObject);
            BossInfo.health--;
            audioSources[3].Play();
        }
    }

}

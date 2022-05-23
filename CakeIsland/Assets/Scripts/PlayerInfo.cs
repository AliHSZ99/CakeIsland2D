using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static int health = 3;
    public bool canShoot;
    public float speed;
    public float jump;
    public GameObject player;
    public PlayerController pc;

    public int timer;
    public bool timerIsActive;

    public float startingTime;
    public float currentTime;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static int points;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingTime = 10f;
        currentTime = startingTime;
        canShoot = true;
        pc = player.GetComponent<PlayerController>();
        speed = pc.movementSpeed;
        jump = pc.jumpingForce;
        timer = 10;
        timerIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        updateLives();

        if (timerIsActive)
        {
            countdownTimer();
        }
    }

    private void updateLives()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void addLife()
    {
        if (health < 3)
        {
            health++;
        }
    }

    /*public void shoot()
    {
        Debug.Log("Powerup has started!");
        timerIsActive = true;
        canShoot = true;
        pc.canShoot = canShoot;
    }*/

    public void speedBoost()
    {
        Debug.Log("Powerup has started!");
        timerIsActive = true;
        speed = 10f;

        pc.movementSpeed = speed;
    }

    public void jumpBoost()
    {
        Debug.Log("Powerup has started!");
        timerIsActive = true;
        jump = 20f;

        pc.jumpingForce = jump;
    }

    private void returnToNormal()
    {
        timerIsActive = false;
        /*canShoot = false;*/
        speed = 5f;
        jump = 14f;
        startingTime = currentTime = 10f;


        /*pc.canShoot = canShoot;*/
        pc.movementSpeed = speed;
        pc.jumpingForce = jump;
    }

    private void countdownTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        if((int)currentTime == 0)
        {
            returnToNormal();   
        }
    }
}

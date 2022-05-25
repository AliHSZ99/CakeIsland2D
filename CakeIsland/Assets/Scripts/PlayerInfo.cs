using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public static int health = 3;
    public bool canShoot;
    public float speed;
    public float jump;
    public GameObject player;
    public PlayerController pc;

    // timer for each powerup
    public int speedTimer;
    public int jumpTimer;

    public bool speedTimerIsActive;
    public bool jumpTimerIsActive;
    public float speedStartingTime;
    public float jumpStartingTime;
    public float speedCurrentTime;
    public float jumpCurrentTime;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static int points;

    // for the ui text
    public TMP_Text timerLabel;

    public float speedTimeLeft;
    public float jumpTimeLeft;
    
    void Start()
    {
        timerLabel.text = "Jump boost: 0 seconds\nSpeed boost: 0 seconds";
        speedTimeLeft = 10f;
        jumpTimeLeft = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        speedStartingTime = jumpStartingTime = 10f;
        speedCurrentTime = speedStartingTime;
        jumpCurrentTime = jumpStartingTime;
        canShoot = true;
        pc = player.GetComponent<PlayerController>();
        speed = pc.movementSpeed;
        jump = pc.jumpingForce;
        speedTimer = 10;
        jumpTimer = 10;
        speedTimerIsActive = jumpTimerIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        updateLives();

        if (!speedTimerIsActive && !jumpTimerIsActive) {
            timerLabel.text = "Jump boost: " + 0 + " seconds\nSpeed boost: " + 0 + " seconds";
        }
        else if (speedTimerIsActive && !jumpTimerIsActive) 
        {
            countdownSpeedTimer();
            setTimerLabel("Speed");
        } else if (!speedTimerIsActive && jumpTimerIsActive) 
        {
            countdownJumpTimer();
            setTimerLabel("Jump");
        } else {
            countdownSpeedTimer();
            countdownJumpTimer();
        }
    }

    private void setTimerLabel(string boost)
    {
        if (boost.Equals("Speed")) {
            timerLabel.text = "Jump boost: " + 0 + " seconds\nSpeed boost: " + (int)speedTimeLeft + " seconds";
        } else if (boost.Equals("Jump")) {
            timerLabel.text = "Jump boost: " + (int)jumpTimeLeft + " seconds\nSpeed boost: " + 0 + " seconds";
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

    public void speedBoost()
    {
        Debug.Log("Powerup has started!");
        speedTimerIsActive = true;
        speed = 10f;

        pc.movementSpeed = speed;
    }

    public void jumpBoost()
    {
        Debug.Log("Powerup has started!");
        jumpTimerIsActive = true;
        jump = 20f;

        pc.jumpingForce = jump;
    }

    private void returnSpeedToNormal()
    {
        speedTimerIsActive = false;
        speed = 5f;
        speedStartingTime = speedCurrentTime = 10f;
        speedTimeLeft = 10f;

        pc.movementSpeed = speed;
    }

    private void returnJumpToNormal() {
        jumpTimerIsActive = false;
        jump = 14f;
        jumpStartingTime = jumpCurrentTime = 10f;
        jumpTimeLeft = 10f;

        pc.jumpingForce = jump;
    }

    private void countdownSpeedTimer()
    {
        speedTimeLeft -= 1 * Time.deltaTime;
        speedCurrentTime -= 1 * Time.deltaTime;
        timerLabel.text = "Jump boost: " + (int)jumpTimeLeft + " seconds\nSpeed boost: " + (int)speedTimeLeft + " seconds";
        if((int)speedCurrentTime == 0)
        {
            returnSpeedToNormal();   
        }
    }

    private void countdownJumpTimer()
    {
        jumpTimeLeft -= 1 * Time.deltaTime;
        jumpCurrentTime -= 1 * Time.deltaTime;
        timerLabel.text = "Jump boost: " + (int)jumpTimeLeft + " seconds\nSpeed boost: " + (int)speedTimeLeft + " seconds";
        if((int)jumpCurrentTime == 0)
        {
            returnJumpToNormal();   
        }
    }
}

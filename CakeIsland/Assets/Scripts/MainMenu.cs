using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script used to handle UI transitions. 
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads the tutorial level scene
    public void PlayGame()
    {
        PlayerInfo.points = 0;
        SceneManager.LoadScene("TutorialLevel");
    }

    // Exits the game no matter which scene it is in
    public void ExitGame()
    {
        PlayerInfo.points = 0;
        Application.Quit();
    }

    // Brings the user to the settings page
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    // Brings the user back to the main menu
    public void BackToMenu()
    {
        PlayerInfo.points = 0;
        SceneManager.LoadScene("MainMenu");
    }

    // Brings the user to the first level
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    // Brings the user to the second level
    public void Level2() {
        SceneManager.LoadScene("Level2");
    }

    // Brings the user to the the final boss level
    public void BossLevel() {
        SceneManager.LoadScene("BossLevel");
    }

    public void WinScreen() {
        PlayerInfo.points = 0;
        SceneManager.LoadScene("WinScreen");
    }

    // Method that lets the player try again. 
    // Here, we are supposed to check which level the player is on and check if they player has enough coins.
    public void TryAgain() {
        PlayerInfo.points = PlayerInfo.points - 100;
        SceneManager.LoadScene(PlayerController.previousScene);
        PlayerInfo.health = 3; 
    }

    // Method used to restart the game from the win screen and game over screen.
    public void RestartGame()
    {
        PlayerInfo.points = 0;
        PlayerInfo.health = 3;
        SceneManager.LoadScene("Level1");
    }

}

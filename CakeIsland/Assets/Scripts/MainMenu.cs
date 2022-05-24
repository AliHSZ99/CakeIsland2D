using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("TutorialLevel");
    }

    // Exits the game no matter which scene it is in
    public void ExitGame()
    {
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
        SceneManager.LoadScene("WinScreen");
    }

    // Method that lets the player try again. 
    // Here, we are supposed to check which level the player is on and check if they player has enough coins.
    public void TryAgain() {
        PlayerInfo.points = PlayerInfo.points - 100;
        SceneManager.LoadScene(PlayerController.previousScene);
        PlayerInfo.health = 3; 
    }

}

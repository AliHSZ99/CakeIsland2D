using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Script for the settings page. 
public class Settings : MonoBehaviour
{
    // variable. 
    public AudioMixer audioMixer;

    // Method used to set the volume of the game. 
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    // Method used to set the screen to maximise or minimise.
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}

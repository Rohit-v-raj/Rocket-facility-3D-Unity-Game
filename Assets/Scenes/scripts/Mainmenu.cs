using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Mainmenu : MonoBehaviour
{ public AudioMixer audioMixer;
   public void Playgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Changevolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
    }
}
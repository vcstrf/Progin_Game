using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Location1");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

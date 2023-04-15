using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Debug");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Play3()
    {
        SceneManager.LoadScene("DIESEL");
    }
    public void Play2()
    {
        SceneManager.LoadScene("EverseeingEye");
    }
    public void Play1()
    {
        SceneManager.LoadScene("");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

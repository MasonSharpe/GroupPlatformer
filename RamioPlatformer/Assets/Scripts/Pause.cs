using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GetComponent<Canvas>().enabled == false)

        {
            if (Time.timeScale != 0)
            {
                //Pause the Game
                PauseGame();
            }
            else
            {
                //Resume Game
                Resume();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Autoload.canvas.GetComponent<AudioSource>().volume = 0.2f;
        GetComponent<Canvas>().enabled = true;
    }
    public void Resume()
    {
        //Resume the game, Unpause
        Autoload.canvas.GetComponent<AudioSource>().volume = 1f;
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        Autoload.canvas.GetComponent<AudioSource>().volume = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMain()
    {
        Time.timeScale = 1;
        Autoload.canvas.GetComponent<AudioSource>().volume = 1f;
        Autoload.canvas.GetComponent<Autoload>().speedrunTimer = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
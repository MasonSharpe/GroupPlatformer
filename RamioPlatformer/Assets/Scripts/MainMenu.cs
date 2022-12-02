using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Autoload manager;
    public GameObject toggleText;
    public GameObject endText;
    public GameObject preManager;
    public bool firstTime;
    void Start()
    {
        

        manager = Autoload.canvas.GetComponent<Autoload>();
        if (manager.firstTime == false)
        {
            Destroy(preManager);
        }
        endText.GetComponent<Text>().text = "Final Time: " + manager.speedrunTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level 1");
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("level " + (manager.level + 1));
    }

    public void toggleTimer()
    {
        manager.timerVisible = !manager.timerVisible;
        if (manager.timerVisible)
        {
            toggleText.GetComponent<Text>().text = "Disable Timer";
        }
        else
        {
            toggleText.GetComponent<Text>().text = "Enable Timer";
        }
    }
}

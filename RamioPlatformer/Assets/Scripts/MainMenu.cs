using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Autoload manager;
    public GameObject endText;
    public GameObject preManager;
    public Toggle timerIsOn;
    public Toggle musicIsOn;
    public Toggle sfxIsOn;
    public Text levelText;
    public bool isWin = false;
    public bool firstTime;
    public GameObject goofyText;
    public AudioClip theme;
    public AudioClip winTheme;
    void Start()
    {
        manager = Autoload.canvas.GetComponent<Autoload>();
        if (SceneManager.GetActiveScene().name == "win2")
        {
            endText.GetComponent<Text>().text = "Final Time: " + (Mathf.Round(manager.speedrunTimer * 100) / 100).ToString();
            if (manager.goofyMode)
            {
                goofyText.GetComponent<Text>().text = "No jump juice needed! You really are a master of this game! Congratulations!!";
            }
        }
        if (isWin)
        {
            if (manager.level == 1)
            {
                levelText.text = "Nice! You fixed the ship! But you need more fuel!";
            }
            else if (manager.level == 2)
            {
                levelText.text = "You escaped the ocean! things are only going to get colder though...";
            }
            else if (manager.level == 3)
            {
                levelText.text = "We can almost leave this planet! But the worst has yet to come!";
            }
            else if (manager.level == 4)
            {
                levelText.text = "You have gotten into space! We just need a little more juice! This is it!";
            }
            else if (manager.level == 5)
            {
                levelText.text = "You got all the fuel! You win! But... you find excess parts on a planet to sell. This will be the hardest challenge yet.";
            }
            else if (manager.level == 6)
            {
                levelText.text = "You got the parts, flew home, sold them off, and now you are rich! People will speak of your tale for generations!!";
            }
        }
        Autoload.canvas.GetComponent<AudioSource>().Stop();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Autoload.canvas.GetComponent<AudioSource>().clip = theme;
            musicIsOn.isOn = manager.musicIsOn;
            sfxIsOn.isOn = manager.sfxIsOn;
            timerIsOn.isOn = manager.timerVisible;
            manager.goofyMode = false;
        }
        else
        {
            Autoload.canvas.GetComponent<AudioSource>().clip = winTheme;
        }
        if (manager.musicIsOn)
        {
            Autoload.canvas.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Autoload.canvas.GetComponent<AudioSource>().Stop();
        manager.musicIsOn = musicIsOn.isOn;
        manager.sfxIsOn = sfxIsOn.isOn;
        manager.timerVisible = timerIsOn.isOn; 
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
        Autoload.canvas.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("level " + (manager.level + 1));
    }


    public void musicToggled()
    {
        if (musicIsOn.isOn)
        {
            Autoload.canvas.GetComponent<AudioSource>().Play();
        }
        else
        {
            Autoload.canvas.GetComponent<AudioSource>().Stop();
        }
    }
}

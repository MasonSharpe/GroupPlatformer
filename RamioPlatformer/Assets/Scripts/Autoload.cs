using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autoload : MonoBehaviour
{
    public static GameObject canvas;
    public GameObject preCanv;
    public int level;
    public float speedrunTimer;
    public bool timerVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = preCanv;
        DontDestroyOnLoad(canvas);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "win" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            speedrunTimer += Time.deltaTime;
        }
    }
}

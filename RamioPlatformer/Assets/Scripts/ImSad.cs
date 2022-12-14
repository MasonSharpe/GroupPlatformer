using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImSad : MonoBehaviour
{
    float loadtime = 0;
    void Start()
    {
        
    }

    private void Update()
    {
        loadtime += Time.deltaTime;
        if (loadtime > 0.5f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

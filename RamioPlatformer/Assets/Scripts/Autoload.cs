using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoload : MonoBehaviour
{
    public static GameObject canvas;
    public GameObject preCanv;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        canvas = preCanv;
        DontDestroyOnLoad(canvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

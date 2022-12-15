using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{

    float timer = 0;
    bool goingUp;
    public Vector2 floatSpeed = new Vector2(0, 0.0001f);
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < -0.8f)
        {
            goingUp = !goingUp;
            timer = 0;
        }
        if (gameObject.tag != "Spaceship" && Time.timeScale == 1)
        {
            if (goingUp)
            {
                transform.Translate(Vector2.up * floatSpeed);
            }
            else
            {
                transform.Translate(Vector2.down * floatSpeed);
            }
        }

    }
}

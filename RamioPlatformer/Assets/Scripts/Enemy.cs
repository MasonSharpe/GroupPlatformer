using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5;
    Rigidbody2D rb;
    public float pathTime;
    float pathTimer;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint.position;
        rb = GetComponent<Rigidbody2D>();
        pathTimer = pathTime;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate((transform.position - endPoint).normalized * speed);
        rb.velocity = (endPoint.position - transform.position).normalized * speed;
        pathTimer -= Time.deltaTime;
        if (pathTimer <= 0)
        {
            pathTimer = pathTime;
            Transform end = endPoint;
            endPoint = startPoint;
            startPoint = end;
        }
    }
}

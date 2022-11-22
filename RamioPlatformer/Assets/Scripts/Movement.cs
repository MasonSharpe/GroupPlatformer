using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float jumpSpeed = 1.0f;
    [SerializeField]
    float rotateSpeed = 50.0f;
    bool grounded = false;
    //Animator animator;
    SpriteRenderer spriteR;
    Rigidbody2D rb;
   // Rigidbody2D feet;
   // public int level;
    float invinsTimer = 0;

    public int health = 100;
    float jumpJuice = 10;

    public GameObject world;
   // float speedJuice = 10;

    //public Slider healthSlider;
    //public Slider jumpSlider;
    //public Slider speedSlider;
   // public GameObject UI;

    //public RuntimeAnimatorController player;
    //public RuntimeAnimatorController playerJ;
    //public RuntimeAnimatorController playerS;
    // Start is called before the first frame update
    void Start()
    {
        //UI.GetComponent<Canvas>().enabled = true;
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, 300 * jumpSpeed));
            grounded = false;
            //animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
	{
        float dist = Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y);
        if (Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.Rotate(new Vector3(0, 0, 1));
            world.transform.Rotate(new Vector3(0, 0, (100 / 2 / Mathf.PI / dist)));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(new Vector3(0, 0, -1));
            world.transform.Rotate(new Vector3(0, 0, -(100 / 2 / Mathf.PI / dist)));
        }
        if (transform.position.y < -100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        float moveX = Input.GetAxis("Horizontal");
        invinsTimer -= Time.deltaTime;
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if (rb.velocity.y < -0.1f && !grounded)
        {
            //animator.SetTrigger("Fall");
        }
       // animator.SetFloat("xInput", moveX);
       // animator.SetBool("Grounded", grounded);
        if (moveX < 0)
        {
            spriteR.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteR.flipX = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (jumpJuice > 0)
           {
              //jumpJuice -= 0.12f;
              rb.gravityScale = 0.2f;
           }
           else
           {
              rb.gravityScale = 1f;
              jumpJuice = -5;
           }
        }
       else
       {
           rb.gravityScale = 1f;
       }
      //  healthSlider.value = health;
       // jumpSlider.value = jumpJuice;
       // speedSlider.value = speedJuice;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        } else if (collision.gameObject.layer == 9)
        {
            //SceneManager.LoadScene("Level" + (level + 1));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damage" && invinsTimer <= 0)
        {
            health -= 15;
            invinsTimer = 1;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

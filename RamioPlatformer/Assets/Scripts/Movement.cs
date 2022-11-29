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
    int parts = 0;
    int progressPowerups = 0;
    public int partsNeeded;
    public int level;
    public GameObject healthSlider;
    public GameObject rotateText;
    public GameObject compass;
    public GameObject partsText;
    public GameObject scaleText;
    public GameObject slider;
    public GameObject tutText;
    Animator animator;
    SpriteRenderer spriteR;
    Rigidbody2D rb;
    // Rigidbody2D feet;
    float heavyScale = 1;
    float invinsTimer = 0;
    float tutTimer = 0;

    public int health = 100;
    float jumpJuice = 10;

    public GameObject world;


    //public RuntimeAnimatorController player;
    //public RuntimeAnimatorController playerJ;
    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        if (level > 1)
        {
            progressPowerups = 2;
        }
        healthSlider.GetComponent<Slider>().value = health;
    }

    private void Update()
    {
        print(grounded);
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, 300 * jumpSpeed));
            grounded = false;
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
	{
        float dist = Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y);
        if (Input.GetKey(KeyCode.LeftArrow) && progressPowerups > 1)
		{
            world.transform.Rotate(new Vector3(0, 0, (120 / 2 / Mathf.PI / dist)));
        }
        if (Input.GetKey(KeyCode.RightArrow) && progressPowerups > 1)
        {
            world.transform.Rotate(new Vector3(0, 0, -(120 / 2 / Mathf.PI / dist)));
        }
        if (transform.position.y < -200)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        compass.transform.rotation = world.transform.rotation;
        partsText.GetComponent<Text>().text = (partsNeeded - parts).ToString() + " Left";
        scaleText.GetComponent<Text>().text = (Mathf.Round((1f - (dist / 100f)) * 100) / 100).ToString() + "x";
        if (dist > 100)
        {
            scaleText.GetComponent<Text>().text = "0.01x";
        }
        rotateText.GetComponent<Text>().text = Mathf.Round(world.transform.rotation.eulerAngles.z).ToString() + "°";
        slider.GetComponent<Slider>().value = jumpJuice;
        healthSlider.GetComponent<Slider>().value = health;
        float moveX = Input.GetAxis("Horizontal");
        invinsTimer -= Time.deltaTime;
        tutTimer -= Time.deltaTime;
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if (tutTimer < 0 && tutText.activeInHierarchy)
        {
            tutText.SetActive(false);
        }
        if (rb.velocity.y < -0.1f && !grounded)
        {
            animator.SetTrigger("Fall");
        }
       animator.SetFloat("xInput", moveX);
       animator.SetBool("Grounded", grounded);
        if (moveX < 0)
        {
            spriteR.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteR.flipX = false;
        }
        if (Input.GetKey(KeyCode.UpArrow) && progressPowerups > 0)
        {
            if (jumpJuice > 0)
           {
              jumpJuice -= 0.03f;
              rb.gravityScale *= 0.2f;
           }
           else
           {
              rb.gravityScale = heavyScale;
              jumpJuice = -5;
           }
        }
       else
       {
           rb.gravityScale = heavyScale;
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            heavyScale = 2;
            print(heavyScale);
        }
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        else if (collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene("level " + (level + 1));
        }
        else if (collision.gameObject.tag == "Pickup")
        {
            jumpJuice = 10;
            health = 100;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Part")
        {
            parts += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "ProgressPowerup")
        {
            progressPowerups++;
            Destroy(collision.gameObject);
            tutText.SetActive(true);
            tutTimer = 5;
            if (progressPowerups == 1)
            {
                tutText.GetComponent<Text>().text = "Hold Up Arrow to active low gravity mode! But be careful! You only have a limited amount!";
            }
            else if (progressPowerups == 2)
            {
                tutText.GetComponent<Text>().text = "Try pressing the left and right arrows...";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spaceship" && parts >= partsNeeded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Water")
        {
            heavyScale = 1;
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

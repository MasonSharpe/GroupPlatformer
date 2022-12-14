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
    bool isTouchingHazard = false;
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
    public GameObject tutPanel;
    public GameObject tempSlider;
    public GameObject speedTimer;
    public Transform water1;
    public Transform water2;
    public Autoload manager;
    public AudioClip music;
    Animator animator;
    SpriteRenderer spriteR;
    Rigidbody2D rb;
    // Rigidbody2D feet;
    float heavyScale = 1;
    float invinsTimer = 0;
    float redTimer = 0;
    float tutTimer = 0;
    float coldTimer = 150f;

    public int health = 100;
    float jumpJuice = 10;

    public GameObject world;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        manager = Autoload.canvas.GetComponent<Autoload>();
        if (!Autoload.canvas.GetComponent<AudioSource>().isPlaying)
        {
            Autoload.canvas.GetComponent<AudioSource>().Stop();
            Autoload.canvas.GetComponent<AudioSource>().PlayOneShot(music);
        }
        manager.level = level;
        if (level > 1)
        {
            progressPowerups = 2;
        }
        healthSlider.GetComponent<Slider>().value = health;
        if (!manager.timerVisible)
        {
            speedTimer.SetActive(false);
        }
    }

    private void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (level == 6)
            {
                SceneManager.LoadScene("win2");
            }
            else
            {
                SceneManager.LoadScene("win");
            }
        }
        if (level == 3)
        {
            if (heavyScale == 2)
            {
                coldTimer -= Time.deltaTime * 2;
            }
            coldTimer -= Time.deltaTime;
        }
        if (level == 2)
        {
            if (transform.position.y < water1.position.y && transform.position.y > water2.position.y)
            {
                coldTimer -= Time.deltaTime * 1.75f;
                heavyScale = 2;
            }
            else
            {
                heavyScale = 1;
            }
        }
        if (coldTimer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (invinsTimer <= 0 && isTouchingHazard)
        {
            health -= 15;
            spriteR.color = new Color(1, 0, 0);
            redTimer = 0.3f;
            invinsTimer = 1;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
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
        if ((transform.position.y < -200 && level != 5) || (transform.position.y < -300 && level == 5))
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
        tempSlider.GetComponent<Slider>().value = coldTimer;
        redTimer -= Time.deltaTime;
        speedTimer.GetComponent<Text>().text = (Mathf.Round(manager.speedrunTimer * 100) / 100).ToString();
        float moveX = Input.GetAxis("Horizontal");
        invinsTimer -= Time.deltaTime;
        tutTimer -= Time.deltaTime;
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if (tutTimer < 0 && tutPanel.activeInHierarchy)
        {
            tutPanel.SetActive(false); 
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
              spriteR.color = new Color(0, 1, 1);
              rb.gravityScale = heavyScale * 0.2f;
           }
           else
           {
              rb.gravityScale = heavyScale;
              spriteR.color = new Color(1, 1, 1);
              jumpJuice = -5;
           }
        }
       else
       {
           rb.gravityScale = heavyScale;
           spriteR.color = new Color(1, 1, 1);
       }
       if (level == 5)
            {
                rb.gravityScale *= 0.25f;
            }
       if (redTimer < 0)
       {
            if (spriteR.color.r != 0)
            {
                spriteR.color = new Color(1, 1, 1);
            }
        }
        else
        {
            spriteR.color = new Color(1, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            heavyScale = 2;
            
        }
        if (collision.gameObject.layer == 3)
        {
            grounded = true;
        }
        else if (collision.gameObject.tag == "Spaceship" && parts >= partsNeeded)
        {
            if (level == 6)
            {
                SceneManager.LoadScene("win2");
            }
            else
            {
                SceneManager.LoadScene("win");
            }
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
            tutPanel.SetActive(true);
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
        else if (collision.gameObject.tag == "Unfreeze")
        {
            Destroy(collision.gameObject);
            coldTimer += 100;
            if (coldTimer > 150)
            {
                coldTimer = 150;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Water")
        {
            heavyScale = 1;
        }
        if (collision.gameObject.tag == "Damage")
        {
            isTouchingHazard = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damage" && invinsTimer <= 0)
        {
            isTouchingHazard = true;
        }
        if (collision.gameObject.tag == "Laser")
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

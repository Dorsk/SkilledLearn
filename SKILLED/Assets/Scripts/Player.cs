using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PhysicsObject
{

    public float maxSpeed = 800;
    public float jumpTakeOffSpeed = 800;
    public Text countText;
    public Text winText;
    public Text timerText;

    private float startTime;
    private bool timer = true;

    private bool moveLeft = false;
    private bool moveRight = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int count;              //Integer to store the number of pickups collected so far.


    // Use this for initialization
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
         
        //Initialize count to zero.
        count = 0;
        // Init timer
        startTime = Time.time;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = ""; 
        SetCountText();

    }
     
    private void finishTimer()
    {
        timerText.color = Color.red;
        timer = false;
    }

    protected override void ComputeVelocity()
    {
        // MOUVEMENT
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
         
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

       
        if (moveLeft)
        { 
            move.x =  (float) -0.99;
            targetVelocity = move * maxSpeed;

        } else if (moveRight)
        { 
            move.x = (float) 0.99;
            targetVelocity = move * maxSpeed;
        }
         
        targetVelocity = move * maxSpeed;

        // TIMER 
        if (timer)
        {
            float t = Time.time;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + " : " + seconds;
        }
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUpCoin"))
        {
            other.gameObject.SetActive(false);
            //Add one to the current value of our count variable.
            count = count + 1;

            //Update the currently displayed count by calling the SetCountText function.
            SetCountText();
        }  
    }


    void SetCountText()
    {
        
        countText.text =  count.ToString() + " / 8";
      
        if (count >= 8)
        {
            winText.text = "You win!";
            finishTimer();
        }
    }

    public void jump()
    {
        if (grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
    }


    public void leftPressed()
    { 
        
        moveLeft = true;
    }

    public void leftReleased()
    {

        moveLeft = false;
    }

    public void rightPressed()
    {

        moveRight = true;
    }

    public void rightReleased()
    {

        moveRight = false;
    }
     
}
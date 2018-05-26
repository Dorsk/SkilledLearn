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

    private bool moveLeft = false;
    private bool moveRight = false;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int count;              //Integer to store the number of pickups collected so far.


    // Use this for initialization
    void Awake()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
         
        //Initialize count to zero.
        count = 0;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = ""; 
        SetCountText();

    }

    protected override void ComputeVelocity()
    {
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


    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text =  count.ToString() + " / 10";
        Debug.Log(count.ToString() + " / 10");
        //Check if we've collected all 12 pickups. If we have...
        if (count >= 5)
        {
            winText.text = "You win!";
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
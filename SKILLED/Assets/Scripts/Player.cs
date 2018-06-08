﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : PhysicsObject
{

    public float maxSpeed = 800;
    public float jumpTakeOffSpeed = 800;
    public Text countText;
    public Text winText;
    public Text timerText;
    public InputField inputName;

     

    public Button nextButton;

    private float startTime;
    private bool timer = false;
    string minutes;
    string seconds;

    private bool moveLeft = false;
    private bool moveRight = false;
     
    private Animator animator;

    private int count;              //Integer to store the number of pickups collected so far.

     
    
    // Use this for initialization
    void Awake()
    {
         
        animator = GetComponent<Animator>();
         
        //Initialize count to zero.
        count = 0;

        // Init timer
        timer = true;
        timerText.text = ""; 
        startTime = Time.time;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = ""; 
        SetCountText();

        // init next button
        nextButton.gameObject.SetActive(false);

        string getName = PlayerPrefs.GetString("Stored Name", inputName.text);

        if (getName != null)
        {
            inputName.text = getName; // show the current SavedNAME in the GUI.TextArea
        }

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

        if (Input.GetKey(KeyCode.A))
        {
            animator.Play("Rogue_run_01");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.Play("Rogue_run_01");
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.D) && !Input.GetButtonDown("Jump") && !moveLeft && !moveRight)
        {
            animator.Play("Rogue_idle_01");
        }

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
            Debug.Log("Rogue_run_01");
            animator.Play("Rogue_run_01");

        } else if (moveRight)
        { 
            move.x = (float) 0.99;
            targetVelocity = move * maxSpeed;
            Debug.Log("Rogue_run_01");
            animator.Play("Rogue_run_01");
        }
        

        targetVelocity = move * maxSpeed;

        // TIMER 
        if (timer)
        {
            float t = Time.time - startTime;

            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");

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
        if (other.gameObject.CompareTag("wall"))
        {
            grounded = false;
        }
    }

    private void SetCountText()
    {
        
        countText.text =  count.ToString() + " / 8";
      
        if (count >= 8)
        {
            winText.text = "You win!";
            finishTimer();
            sendValues();

            // init next button
            nextButton.gameObject.SetActive(true);
             
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

    public void sendValues()
    {
        
        if (inputName.text == null || inputName.text == "")
        { 
            UnityWebRequest request = UnityWebRequest.Get("http://dorsk.fr/game/add.php?minutes=" + minutes + "&secondes=" + seconds + "&name=Joueur1&level=" + (SceneManager.GetActiveScene().buildIndex - 2));
            request.Send();
        }
        else
        { 
            UnityWebRequest request = UnityWebRequest.Get("http://dorsk.fr/game/add.php?minutes=" + minutes + "&secondes=" + seconds + "&name=" + inputName.text + "&level=" + (SceneManager.GetActiveScene().buildIndex - 2));
            request.Send();

            PlayerPrefs.SetString("Stored Name", inputName.text);
        } 
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
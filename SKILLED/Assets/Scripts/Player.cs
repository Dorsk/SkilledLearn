using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsObject
{

    public float maxSpeed = 800;
    public float jumpTakeOffSpeed = 800;

    private bool moveLeft = false;
    private bool moveRight = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
     

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
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
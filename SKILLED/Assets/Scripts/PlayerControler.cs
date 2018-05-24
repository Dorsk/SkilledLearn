using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : PhysicsObject {

    private float maxspeed = 6;
    private float jumpTakeOffSpeed = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonDown("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }


        targetVelocity = move * maxspeed;
    }
}


using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    
    public Button btnLeft;
    public Button btnRight; 
    public Button btnJump;
    public Button btnBack;
      
    private Vector3 offsetL;
    private Vector3 offsetR;
    private Vector3 offsetJ;
    private Vector3 offsetB; 
     
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        /*offsetB  = btnBack.transform.position - player.transform.position;
        offsetR = btnBack.transform.position - player.transform.position;
        offsetL = btnBack.transform.position - player.transform.position;
        offsetJ = btnBack.transform.position - player.transform.position;*/
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

        /*btnBack.transform.position = btnBack.transform.position + offsetB;
        btnRight.transform.position = btnRight.transform.position + offset;
        btnLeft.transform.position = btnLeft.transform.position + offset;
        btnJump.transform.position = btnJump.transform.position + offset;
        */
         
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    public Text winText;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainHome()
    {
        winText.text = "";
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

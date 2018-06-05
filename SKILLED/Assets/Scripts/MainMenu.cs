using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    public Text winText;

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }

    public void ChooseLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(1);
    }

    public void MainHome()
    {
        winText.text = "";
        SceneManager.LoadScene(0);
    }


    public void OpenStats()
    {
        Application.OpenURL("http://dorsk.fr/game/Skilled.php");
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


    public void Level1()
    {
        SceneManager.LoadScene(3);
    }
    public void Level2()
    {
        SceneManager.LoadScene(4);
    }
    public void Level3()
    {
        SceneManager.LoadScene(5);
    }

}

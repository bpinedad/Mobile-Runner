using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioClip click;
    GameManager gameManager;

    //Get GameManager
    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Exit when clicked
    public void PlayPressed(){
		Debug.Log ("Start Play");
        gameManager.PlayAudio(click);
        SceneManager.LoadScene("Play");
	}

    //Exit when clicked
    public void ExitPressed(){
		Debug.Log ("Exiting application!");
        gameManager.PlayAudio(click);
        Application.Quit();
	}

    //Back to Menu
    public void MenuPressed(){
		Debug.Log ("Back to menu!");
        gameManager.PlayAudio(click);
        SceneManager.LoadScene("StartScreen");
	}

}

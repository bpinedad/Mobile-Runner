using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Exit when clicked
    public void PlayPressed(){
		Debug.Log ("Start Play");
        SceneManager.LoadScene("Play");
	}

    //Exit when clicked
    public void SettingsPressed(){
		Debug.Log ("Start Settings");
        Application.Quit();
	}

    //Exit when clicked
    public void ShopPressed(){
		Debug.Log ("Start shop");
	}

    //Exit when clicked
    public void ExitPressed(){
		Debug.Log ("Exiting application!");
        Application.Quit();
	}

    // Start is called before the first frame update
    void Start()
    {
    }

    
}

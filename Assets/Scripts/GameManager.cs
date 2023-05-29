using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance {get { return _instance; } }
    int score = 0;

    void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Handle click sounds
    public void PlayAudio(AudioClip clip){
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    //Handle Score
    public void UpdateScore(int s) {
        score = s;
    }

    public int GetScore() {
        return score;
    }
}

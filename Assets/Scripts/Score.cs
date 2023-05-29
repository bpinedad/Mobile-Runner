using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreLabel;
    int score = 0;
    GameObject player;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update score value
        if (score < (int)player.transform.position.x) {
            score = (int)player.transform.position.x;

             //Update label
            scoreLabel.text = $"Score: {score}";
            gameManager.UpdateScore(score);
        }
    }
}

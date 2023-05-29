using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    public TMP_Text scoreLabel;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Only display score once
        int score = gameManager.GetScore();
        scoreLabel.text = $"Score: {score}";
    }
}

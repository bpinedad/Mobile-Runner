using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    float yUpperLimit = 15f;
    float yLowerLimit = -5f;

    bool shot = false;

    AudioSource audioSource;
    [SerializeField] AudioClip deadSFX;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If exceeding any limit, stop and move to game over screen
        if (transform.position.y < yLowerLimit || transform.position.y > yUpperLimit) {

            //Wait for sec and stop player
            StartCoroutine(EndGame());          
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //Just collided with bullet
        Debug.Log("Collided with player");

        //Play these only on first hit
        if (!shot) {
            //Play dead sound
            audioSource.PlayOneShot(deadSFX);

            //Play particles
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Play();
        }

        //End game
        StartCoroutine(EndGame());

    }
    //Change to gameover screen
    IEnumerator EndGame() {
        //Avoid repeating animaiton for dead
        if (shot) {
            yield return 0;
        }

        //Indicate player is dead already
        shot = true;

        //Stop player controls
        PlayerMove playerController = GetComponent<PlayerMove>();
        playerController.dead = true;
        //playerController.enabled = false;

        //Wait
        yield return new WaitForSeconds(3);

        //Load scene
        SceneManager.LoadScene("GameOver");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speedX = 10f;
    [SerializeField] public float gravityForce = 150f;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get current scales
        Vector3 currentLocalScale = player.transform.localScale;

        if (Input.GetKey("left"))
        {
            transform.Translate(new Vector3(-speedX * Time.deltaTime, 0f, 0f ), Space.World);
            animator.SetBool("Running", true);
            currentLocalScale.z = -1; 
        }

        else if (Input.GetKey("right"))
        {
            transform.Translate(new Vector3(speedX * Time.deltaTime, 0f, 0f ), Space.World);
            animator.SetBool("Running", true);
            currentLocalScale.z = 1; 
        }

        else {
            animator.SetBool("Running", false);
        }

        if (Input.GetKeyDown("space"))
        {
            // Toggle gravity
            gravityForce *= -1;
            animator.SetBool("Floating", true);

            Debug.Log($"Before {transform.position}");
            transform.Translate(new Vector3(0f, -(gravityForce/Mathf.Abs(gravityForce)) * 2f , 0f ), Space.World);
            Debug.Log($"After {transform.position}");
            transform.Rotate(Vector3.forward, 180f, Space.Self);
            Debug.Log($"After after {transform.position}");
        }

        //Update scale
        player.transform.localScale = currentLocalScale;

        // To only adjust one object gravity we will use it as a force per object
        GetComponent<Rigidbody>().AddForce( -transform.up * Mathf.Abs(gravityForce) * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce( -transform.up * gravityForce * Time.deltaTime);
    }

    // Stop floating when landing
    private void OnCollisionEnter(Collision other) {
        animator.SetBool("Floating", false);
    }
}
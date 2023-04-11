using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speedX = 10f;
    [SerializeField] float speedRotation = 180f;
    [SerializeField] public float gravityForce = 150f;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject player;
    private float gravityDirection;
    private float movingAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update gravity direction
        gravityDirection = gravityForce/Mathf.Abs(gravityForce);

        //Get currentRotation scales
        Vector3 currentLocalScale = player.transform.localScale;

        //Calculate moving direction and amount
        //Only enter when first pressed
        if (Input.GetKeyDown("left"))
        {
            movingAmount = -speedX * Time.deltaTime;
            animator.SetBool("Running", true);
            currentLocalScale.z = -1; 
        }

        else if (Input.GetKeyDown("right"))
        {
            movingAmount = speedX * Time.deltaTime;
            animator.SetBool("Running", true);
            currentLocalScale.z = 1; 
        }

        // Only when none is pressed
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            movingAmount = 0;
            animator.SetBool("Running", false);
        }
        transform.Translate(new Vector3(movingAmount, 0f, 0f ), Space.World);
        Debug.Log($"Moving force is {movingAmount}");

        if (Input.GetKeyDown("space"))
        {
            // Toggle gravity
            gravityForce *= -1;
            gravityDirection *= -1;
            animator.SetBool("Floating", true);

            transform.Translate(new Vector3(0f, -gravityDirection * 2f, 0f ), Space.World);
            transform.Rotate(Vector3.forward, 180f, Space.Self);
            //StartCoroutine(RotateFloating());
        }

        //Update scale
        player.transform.localScale = currentLocalScale;

        // To only adjust one object gravity we will use it as a force per object
        // For player the force is absolute since we rotate the whole object
        GetComponent<Rigidbody>().AddForce( -transform.up * Mathf.Abs(gravityForce) * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce( -transform.up * gravityForce * Time.deltaTime);
    }

    //Avoid clipping obstacles
    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Wall") {
            // Neglect movement
            Debug.Log("Trying");
            transform.Translate(new Vector3(-movingAmount, 0f, 0f ), Space.World);
            //movingAmount = 0;
        }
    }

    //Rotate floating
    IEnumerator RotateFloating() {

        //Need t adjust y position while doing this too

        //conditionA = gravityDirection == 1 && transform.rotation.z < 180f
        //conditionB = gravityDirection == -1 && transform.rotation.z > 0f
        //while ( (gravityDirection == 1 && transform.rotation.z < 180f) || (gravityDirection == -1 && transform.rotation.z > 0f) ) {
        transform.Translate(new Vector3(0f, -gravityDirection * 2f, 0f ), Space.World);
        while (true) {
            Debug.Log($"Coroutine with gravity dir: {gravityDirection}, rotation of {transform.rotation} and position of {transform.position}");

            var currentRotation = transform.rotation;
            var currentPosition = transform.position;
            float rotationStep = speedRotation * Time.deltaTime;

            //Clamp value to 0 or 180
            if (gravityDirection == -1) {
                if (currentRotation.z + speedRotation * Time.deltaTime > 180f) {
                    currentRotation.z = 180f;
                    currentPosition.y = -2f;
                    transform.rotation = currentRotation;
                    transform.position = currentPosition;
                    break;
                } else {
                    transform.Rotate(Vector3.forward, gravityDirection * rotationStep, Space.Self);
                    //transform.Translate(new Vector3(0f, -gravityDirection * 2f * Time.deltaTime, 0f ), Space.World);
                }
                
            } else if (gravityDirection == 1) {
                if (currentRotation.z - speedRotation * Time.deltaTime < 0f) {
                    currentRotation.z = 0f;
                    currentPosition.y = 0f;
                    transform.rotation = currentRotation;
                    transform.position = currentPosition;
                    break;
                } else {
                    transform.Rotate(Vector3.forward, gravityDirection * rotationStep, Space.Self);
                    //transform.Translate(new Vector3(0f, -gravityDirection * 2f * Time.deltaTime, 0f ), Space.World);
                }            
            }

            yield return null;
        }
    }
}
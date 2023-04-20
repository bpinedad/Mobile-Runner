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
    bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movingAmount = 0;

        //Update gravity direction
        gravityDirection = gravityForce/Mathf.Abs(gravityForce);

        //Get currentRotation scales
        Vector3 currentLocalScale = player.transform.localScale;
        Vector3 temp = transform.rotation.eulerAngles;

        //Calculate moving direction and amount
        //Only enter when first pressed
        if (Input.GetKey("left"))
        {
            movingAmount = speedX * Time.deltaTime;
            animator.SetBool("Running", true);
            //currentLocalScale.z = -1; 
            temp.y = -90.0f;
        }

        else if (Input.GetKey("right"))
        {
            movingAmount = speedX * Time.deltaTime;
            animator.SetBool("Running", true);
            //currentLocalScale.z = 1; 
            temp.y = 90.0f;
        }

        // Only when none is pressed
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            movingAmount = 0;
            animator.SetBool("Running", false);
        }

        // Move forward if nothing in front
        WatchForward(movingAmount);
        
        if (Input.GetKeyDown("space"))
        {
            // Toggle gravity
            gravityForce *= -1;
            gravityDirection *= -1;
            animator.SetBool("Floating", true);

            transform.Translate(new Vector3(0f, -gravityDirection * 2f, 0f ), Space.World);
            //transform.Rotate(Vector3.forward, 180f, Space.Self);
            temp.x += 180.0f;
            temp.y *= -1;
            //StartCoroutine(RotateFloating());
        }

        //Update scale
        player.transform.localScale = currentLocalScale;
        transform.rotation = Quaternion.Euler(temp);

        // To only adjust one object gravity we will use it as a force per object
        // For player the force is absolute since we rotate the whole object
        GetComponent<Rigidbody>().AddForce( -transform.up * Mathf.Abs(gravityForce) * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce( -transform.up * gravityForce * Time.deltaTime);
    }

    //Rotate floating
    IEnumerator RotateFloating() {

        //Need t adjust y position while doing this too

        //conditionA = gravityDirection == 1 && transform.rotation.z < 180f
        //conditionB = gravityDirection == -1 && transform.rotation.z > 0f
        //while ( (gravityDirection == 1 && transform.rotation.z < 180f) || (gravityDirection == -1 && transform.rotation.z > 0f) ) {
        transform.Translate(new Vector3(0f, -gravityDirection * 2f, 0f ), Space.World);
        while (true) {
            //Debug.Log($"Coroutine with gravity dir: {gravityDirection}, rotation of {transform.rotation} and position of {transform.position}");

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

    private void WatchForward (float movingAmount){
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 offset = new Vector3(0.0f, 0.7f * gravityDirection, 0.0f);
        Vector3 offsetHead = new Vector3(0.0f, 1.5f * gravityDirection, 0.0f);
        Vector3 offsetFeet = new Vector3(0.0f, -0.6f * gravityDirection, 0.0f);

        if (Physics.Raycast(transform.position + offset, fwd, 1.2f)) 
            Debug.DrawRay(transform.position + offset, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        else
            Debug.DrawRay(transform.position + offset, transform.TransformDirection(Vector3.forward) * 1, Color.green);

        if (Physics.Raycast(transform.position + offsetHead, fwd, 1.2f)) 
            Debug.DrawRay(transform.position + offsetHead, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        else
            Debug.DrawRay(transform.position + offsetHead, transform.TransformDirection(Vector3.forward) * 1, Color.green);

        if (Physics.Raycast(transform.position + offsetFeet, fwd, 1.2f)) 
            Debug.DrawRay(transform.position + offsetFeet, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        else
            Debug.DrawRay(transform.position + offsetFeet, transform.TransformDirection(Vector3.forward) * 1, Color.green);

        if (!Physics.Raycast(transform.position + offset, fwd, 1) && !Physics.Raycast(transform.position + offsetHead, fwd, 1) && !Physics.Raycast(transform.position + offsetFeet, fwd, 1)) {
            Debug.Log($"Moving {movingAmount}");
            transform.Translate(new Vector3(0f, 0f, movingAmount ), Space.Self);
        }
    }
}
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

    public bool dead = false;
    bool deadComplete = false;

    [SerializeField] Feet myFeet;
    Rigidbody rb;

    [SerializeField] float movingAmount = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movingVector = new Vector3(0f, 0f, 0f);
        Vector3 gravityVector = new Vector3(0f, 0f, 0f);

        //Apply gravity
        gravityVector = -transform.up * Mathf.Abs(gravityForce);
        rb.AddForce( (gravityVector) * Time.deltaTime);

        //Avoid other actions if dead
        if (dead){
            if (!deadComplete)
                Die();
            return;
        }        

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
            movingVector = transform.forward * speedX;
            animator.SetBool("Running", true);
            //currentLocalScale.z = -1; 
            temp.y = -90.0f;
        }

        else if (Input.GetKey("right"))
        {

            movingAmount = speedX * Time.deltaTime;
            movingVector = transform.forward * speedX;
            animator.SetBool("Running", true);
            //currentLocalScale.z = 1; 
            temp.y = 90.0f;
        }

        // Only when none is pressed
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            movingAmount = 0f;
            movingVector = new Vector3(0f, 0f, 0f);
            animator.SetBool("Running", false);
        }

        // Move forward if nothing in front
        WatchForward();
        
        if (Input.GetKeyDown("space") && !myFeet.floating)
        {
            // Toggle gravity
            gravityForce *= -1;
            gravityDirection *= -1;
            animator.SetBool("Floating", true);

            transform.Translate(new Vector3(0f, -gravityDirection * 1.5f, 0f ), Space.World);
            //transform.Rotate(Vector3.forward, 180f, Space.Self);
            temp.x += 180.0f;
            temp.y *= -1;
        }

        //Update scale
        player.transform.localScale = currentLocalScale;
        transform.rotation = Quaternion.Euler(temp);

        // To only adjust one object gravity we will use it as a force per object
        // For player the force is absolute since we rotate the whole object
        //rb.AddForce( -transform.up * Mathf.Abs(gravityForce) * Time.deltaTime);
        //rb.AddForce( new Vector3(movingAmount, 0f, -Mathf.Abs(gravityForce)) * Time.deltaTime);
        
        //rb.MovePosition( (transform.position + movingVector*Time.deltaTime));
        //Debug.Log($"Gravity: {gravityVector}");
        //Debug.Log($"Speed: {movingVector}");
        //GetComponent<Rigidbody>().AddForce( -transform.up * gravityForce * Time.deltaTime);
    }

    private void WatchForward (){
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 offset = new Vector3(0.0f, 0.7f * gravityDirection, 0.0f);
        Vector3 offsetHead = new Vector3(0.0f, 1.5f * gravityDirection, 0.0f);
        Vector3 offsetFeet = new Vector3(0.0f, -0.6f * gravityDirection, 0.0f);
        float rayLength = 1.3f;

        //Verify collision of each ray and that hit is of tag wall
        bool rayHit1 = Physics.Raycast(transform.position + offset, fwd, out hit1, rayLength);
        bool rayHit2 = Physics.Raycast(transform.position + offsetHead, fwd, out hit2, rayLength);
        bool rayHit3 = Physics.Raycast(transform.position + offsetFeet, fwd, out hit3, rayLength);


        //Move character
        //float movingAmount = 5f;
        //rb.velocity += new Vector3(0f, 0f, movingAmount );

        if (!rayHit1 && !rayHit2 && !rayHit3
        ) {
            animator.SetBool("Pushing", false);
            transform.Translate(new Vector3(0f, 0f, movingAmount ), Space.Self);
            
        } 
        else {
            //Do push animation only if moving on any direction and colliding with any raycast
            if (Input.GetKey("left") || Input.GetKey("right"))
            {
                animator.SetBool("Pushing", true);
            }
            else {
                animator.SetBool("Pushing", false);
            }

            //Push animation is set depending on if anything is touching, however, will still move if objet allows
            if ((hit3.collider == null || !hit3.collider.CompareTag("Wall")) && 
                (hit2.collider == null || !hit2.collider.CompareTag("Wall")) && 
                (hit1.collider == null || !hit1.collider.CompareTag("Wall"))) {
                
                transform.Translate(new Vector3(0f, 0f, movingAmount ), Space.Self);

                //Adjust collider to new animation

            }
        }
        
    }

    public void Die(){
        deadComplete = true;

        //Push
        Vector3 bulletVector = new Vector3(-1, gravityDirection, 0) * 100;
        rb.AddForce( (bulletVector));

        //Kill
        animator.SetTrigger("Dead");
    }
}
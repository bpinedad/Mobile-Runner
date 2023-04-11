using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float objectDirection;
    private float gravityForce;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ReadCurrentGravity();
            
        // To only adjust one object gravity we will use it as a force per object
        
        // This rotates from the object
        //GetComponent<Rigidbody>().AddForce( -transform.up * gravityForce * Time.deltaTime, ForceMode.Force);

        // This rotates from the world
        GetComponent<Rigidbody>().AddForce( -Vector3.up * gravityForce * Time.deltaTime, ForceMode.Force);
    }

    void ReadCurrentGravity(){

        gravityForce = player.GetComponent<PlayerMove>().gravityForce * objectDirection;
    }
}

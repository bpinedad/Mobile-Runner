using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    GameObject player;
    float objectDirection;
    private float gravityForce;
    GameObject baseTurret;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        baseTurret = GameObject.Find("Turret");
        BuildTurret();
        DefineBehavior();
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

    void BuildTurret()
    {
        //Key values are 0.5 and 0.9 for turret position
        //Probability to convert a block into a turret
        //30% chance per side
        //50% on each direction
        float turretProb = 0.4f;
        float directionProb = 0.5f;

        //Define pos per side
        float[] xPos = new float[4]{0f,  0f, 1f, -1f};
        float[] yPos = new float[4]{1f, -1f, 0f,  0f};
        float[] xRot = new float[4]{180f,    0f,   0f,    0f};
        float[] yRot = new float[4]{0f,      0f,   0f,  180f};
        float[] zRot = new float[4]{270f,   270f,  0f,    0f};

        //Run four times, one per side
        for(int i = 0; i < 4; i++) {
            if (Random.Range(0f, 1f) <= turretProb) {

                //Set direction
                if (Random.Range(0f, 1f) <= directionProb)
                {
                    //For i 0-1 is Y, for above is X
                    if (i < 2) {
                        yRot[i] = 180f;
                    } else {
                        xRot[i] = 180f;
                    }

                }

                //Create turret
                //Vector3 turretPosition = new Vector3(xPos[i], yPos[i], 0f);
                Vector3 turretPosition = new Vector3(xPos[i], yPos[i], 0f) + transform.position;
                Quaternion turretRotation = Quaternion.Euler(new Vector3(xRot[i], yRot[i], zRot[i]));
                var tmpTurret = Instantiate(baseTurret, turretPosition, turretRotation);
                tmpTurret.transform.parent = gameObject.transform;

                
            }
        }

    }

    void DefineBehavior() {
        //Set object gravity at random
        objectDirection = Random.Range(-1, 1); 

        //Set rotation
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,Random.Range(0f, 360f)));
    }
}

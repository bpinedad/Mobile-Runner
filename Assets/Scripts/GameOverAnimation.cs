using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    


        //Set rotation
        transform.Rotate(rotationSpeed/2 * Time.deltaTime, rotationSpeed * 2 * Time.deltaTime, rotationSpeed * Time.deltaTime, Space.Self);
        //transform.locaotation = Quaternion.Euler(new Vector3(0,0, transform.localRotation.z + rotationSpeed * Time.deltaTime));
    }
}

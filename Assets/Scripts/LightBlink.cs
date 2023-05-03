using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    Light myLight;

    bool blinking = false;
    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] float minIntensity = 5f;
    [SerializeField] float maxIntensity = 5f;
    float lastTime = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTime + 5)
        {
            //Test pingpong
            myLight.enabled = !myLight.enabled;
            lastTime = Time.time;
        }

/*
        //If turning on light
        if (blinking) {
            //Increase each frame by speed
            myLight.intensity += blinkSpeed;
            //If at max intensity revert
            if (myLight.intensity > maxIntensity) {
                blinking = false;
            }
            //Opposite
        } else {
            GmyLight.intensity -= blinkSpeed;
            if (myLight.intensity < minIntensity) {
                blinking = true;
            }
        }

*/
        
    }
}

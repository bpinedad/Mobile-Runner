using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [SerializeField] float freq = 1f;
    [SerializeField] float minIntensity = 1f;
    [SerializeField] float maxIntensity = 10f;

    Light myLight;

    // Start is called before the first frame update
    void Awake()
    {
        //Get light component
        myLight = GetComponent<Light>();    
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate sin function
        float sinFunc = Mathf.Sin(freq * Time.time) * 0.5f + 0.5f;

        //Adjust light
        myLight.intensity = minIntensity + maxIntensity * sinFunc;
    }
}

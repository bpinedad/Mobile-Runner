using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Stop floating when landing
    private void OnTriggerEnter(Collider other) {
        animator.SetBool("Floating", false);
    }

    // Start floating when eaving
    private void OnTriggerExit(Collider other) {
        animator.SetBool("Floating", true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] public Animator animator;
    bool floating = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Floating", floating);
    }

    private void OnTriggerStay(Collider other) {
        floating = false;
    }

    // Start floating when eaving
    private void OnTriggerExit(Collider other) {
        floating = true;
    }

}

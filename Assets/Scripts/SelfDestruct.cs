using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    GameObject player;
    [SerializeField] float maxDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.SetParent(GameObject.Find("World").transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy if behind player by X distance
        float distanceToPlayer = player.transform.position.x - transform.position.x;
        if (distanceToPlayer > maxDistance) {
            Destroy(gameObject);
        }
    }
}

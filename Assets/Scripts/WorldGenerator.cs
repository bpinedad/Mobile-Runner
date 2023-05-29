using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    //Tiles
    [SerializeField] GameObject floorTile;
    float xStep = 5f;
    float yUp = 10.4f;
    float yDown = -0.4f;

    //Turrets
    [SerializeField] List<GameObject> turret;

    //Last value used for generation
    int lastStep = -5;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Get player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Get current X position for player
        float currentX = player.transform.position.x;

        //Generate upcoming set of map if player moved enough
        //Use steps of 5
        int currentStep = (int)(currentX / xStep);
        if (currentStep > lastStep )
        {
            lastStep = currentStep;

            //Next objects are added at current + 20
            float newX = lastStep * 5 + xStep * 4;
            Debug.Log($"Current: {currentStep}. Next X: {newX}");

            //Probability to add floor is 65%, but ensure at least one was added
            float tileProbability = 0.65f;
            bool spaceUsed = false;
            //Randomly add top
            if (Random.Range(0f, 1f) <= tileProbability) {
                Instantiate(floorTile, new Vector3(newX, yUp, 0), Quaternion.identity);
            } else
            {
                //Space already used 
                spaceUsed = true;
            }

            //Randomly add bottom. If space used, force adding
            if (spaceUsed || Random.Range(0f, 1f) <= tileProbability) {
                Instantiate(floorTile, new Vector3(newX, yDown, 0), Quaternion.identity);
            }

            //Randomly add turret
            float turretProbability = 0.5f;
            float maxY = yUp - 2f;
            float minY = yDown + 2f;
            float newY = Random.Range(minY, maxY);

            if (Random.Range(0f, 1f) <= turretProbability) {
                //Select random turret
                var block = Instantiate(turret[Random.Range(0, turret.Count)], new Vector3(newX, newY, 0), Quaternion.identity);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Spawns objects that fly towards the player
 */
public class ObjectSpawner : MonoBehaviour
{
    public float heightVariation; // how far the object can spawn from y0
    public GameObject pipePrefab;

    private float totalPipeTime;      // time elapsed since last pipe spawn
    public float pipeSpawnerInterval; // amount of time needed for a pipe to spawn

    private const float TIMETODESTROY = 10; // time until object gets destroyed

    // Update is called once per frame
    void Update()
    {
        // spawn a pipe if the time reaches the spawner interval
        if(totalPipeTime > pipeSpawnerInterval) 
        {
            SpawnPipe();
            totalPipeTime = 0; // reset time so it can climb back up to the spawnerinterval
        }


        // Time.deltaTime counts time since last draw frame
        // this is important to keeping timing in the Update method consistent
        // NOTE: if you want to do this in FixedUpdate, you need to use Time.fixedDeltaTime
        totalPipeTime += Time.deltaTime; 
    }

    void SpawnPipe()
    {
        // creates two random floats between +/- heightvariation
        float height = Random.Range(-heightVariation, heightVariation);
        float height2 = Random.Range(-heightVariation, heightVariation);
        // ensures that the height is typically not near the middle
        if (Mathf.Abs(height) < Mathf.Abs(height2))
            height = height2;

        // sets spawn position and spawns the pipe
        Vector3 SpawnPos = new Vector3(transform.position.x, height, 0);
        GameObject spawnedPipe = Instantiate(pipePrefab, SpawnPos, Quaternion.identity);

        // destroys the pipe after predetermined amount of time in case it goes off screen
        Destroy(spawnedPipe, TIMETODESTROY);
    }
}

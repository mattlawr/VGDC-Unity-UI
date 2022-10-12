using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Shoots spawned objects towards the player
 */
public class ObjectController : MonoBehaviour
{
    public float speed; //speed the object shoots towards the player

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves the pipe towards the player every physics frame
        transform.position += Vector3.left * speed;  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAMPLE_PCNTRLR : MonoBehaviour
{

    public float PipeSpeed = 0.01f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - PipeSpeed,
            transform.position.y);
    }
}

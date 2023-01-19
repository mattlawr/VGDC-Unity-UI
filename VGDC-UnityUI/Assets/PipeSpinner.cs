using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * makes the little pipe do a little spinny spin
 * 
 * (you do not need to edit this file for the workshop)*
 */
public class PipeSpinner : MonoBehaviour
{
    public float spinSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = spinSpeed;
    }
}

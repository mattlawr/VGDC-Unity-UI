using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementController : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score;
    public float jumpSpeed;
    private Rigidbody2D rb;

    private const int OUTOFBOUNDS = 9;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        // resets velocity if player is OOB
        if (transform.position.y >= OUTOFBOUNDS)
            rb.velocity = new Vector2(0, 0);
    }

    /**
     * Checks which inputs the player is pressing and calls the appropriate methods
     */
    void GetInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    /**
     * Applies vertical velocity to the player if they are jumping
     */
    void Jump()
    {
        // doesn't allow the player to jump above the screen
        if (transform.position.y <= OUTOFBOUNDS)
            rb.velocity = new Vector2(0, jumpSpeed);   
    }

    /**
     * Ends the game if a player loses
     */
    void GameOver()
    {
        Debug.Log("GAME OVER! Score: " + score);
        Time.timeScale = 0; // sets the time scale of the game to 0
    }

    /**
     * Increments the player's score if they get past an obstacle
     * is separate from OnCollisionEnter, as triggers do not collide with the player
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hazard"))
        {
            {
                GameOver();
                return;
            }
        }

        score += 1;
        scoreText.text = score.ToString();
    }
}

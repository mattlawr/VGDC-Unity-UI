using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EXAMPLE_MVMNTCNTRLR : MonoBehaviour
{
    private bool shieldOn;

    public TMP_Text scoreText;
    public GameObject GameOverScreen;

    private int score;
    public float jumpSpeed;
    private Rigidbody2D rb;

    private const int OUTOFBOUNDS = 9;
    private const int POWERUPTIME = 5;

    // Start is called before the first frame update
    void Start()
    {
        shieldOn = false;
        GameOverScreen.SetActive(false);
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
        if (Input.GetButtonDown("Jump"))
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
        GameOverScreen.SetActive(true);
        Debug.Log("GAME OVER! Score: " + score);
        Time.timeScale = 0; // sets the time scale of the game to 0
    }

    /**
     * Checks what the player is colliding with
     * If it is a pipe, kill the player
     * If it is the area around a pipe, increment the score
     * If it is a powerup, do other things
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hazard"))
        {
            if (shieldOn)
                Destroy(collider.gameObject);
            else
            {
                GameOver();
                return;
            }
        }

        else if (collider.CompareTag("ShieldPowerup"))
        {
            Destroy(collider.gameObject);
            shieldOn = true;
            StartCoroutine(disableShield());
        }
        score += 1;
        scoreText.text = score.ToString();


    }

    IEnumerator disableShield()
    {
        yield return new WaitForSeconds(POWERUPTIME);
        shieldOn = false;
    }
}

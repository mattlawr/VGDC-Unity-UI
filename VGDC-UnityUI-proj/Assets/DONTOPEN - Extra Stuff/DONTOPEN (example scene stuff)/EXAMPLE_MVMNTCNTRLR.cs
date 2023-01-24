using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EXAMPLE_MVMNTCNTRLR : MonoBehaviour
{
    private bool shieldOn;
    public GameObject shieldObjectOverlay;

    public TMP_Text scoreText;
    public GameObject GameOverScreen;
    private bool gameOver = false;

    private int score;
    public float jumpSpeed;
    private Rigidbody2D rb;

    public GameObject PauseScreen;
    private bool gamePaused = false;    // Note -- this is kinda awkward to put in a player movement script FYI

    private const int OUTOFBOUNDS = 9;
    private const float POWERUPTIME = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        shieldOn = false;
        GameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        if (PauseScreen) PauseScreen.SetActive(false);
        else Debug.LogWarning("Pause menu reference not set. Please check Player object in inspector");
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

        // UI stuff vvv
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
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
     * Toggles the game's pause state
     */
    public void Pause()
    {
        if(gameOver)
        {
            Debug.Log("Pause game failed: cannot pause after game over!");
            return;
        }

        gamePaused = !gamePaused;   // toggle the bool

        PauseScreen.SetActive(gamePaused);
        
        Time.timeScale = gamePaused ? 0 : 1; // sets the game to either freeze (0) or play (1)
        /// the above is equivalent to:
        /// if(gamePaused) Time.timescale = 0; else Time.timescale = 1;
        /// this is called a ternary operator!
    }


    /**
     * Ends the game if a player loses
     */
    void GameOver()
    {
        gameOver = true;
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
            return;
        }

        else if (collider.CompareTag("ShrinkPowerup"))
        {
            Destroy(collider.gameObject);
            transform.localScale = transform.localScale * 0.5f;
            StartCoroutine(unShrink());
            return;
        }
        
        score += 1;
        scoreText.text = score.ToString();


    }

    IEnumerator disableShield()
    {
        GameObject spawnedShield = Instantiate(shieldObjectOverlay, transform);
        spawnedShield.transform.parent = transform;
        yield return new WaitForSeconds(POWERUPTIME);
        shieldOn = false;
        Destroy(spawnedShield);
    }

    IEnumerator unShrink()
    {
        yield return new WaitForSeconds(POWERUPTIME);
        transform.localScale = transform.localScale * 2f;
    }
}

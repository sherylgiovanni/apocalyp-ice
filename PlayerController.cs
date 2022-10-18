using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     // Sheryl Tania
    private float horizontalInput;
    private float verticalInput;
    public float speed = 25.0f;
    public float xRange = 30;
    public float zRange = -50;
    private int projectileCount;
    public GameObject projectilePrefab;
    private Rigidbody playerRb;
    public SpawnManager spawnManager;
    private GameObject[] projectiles;
    public ParticleSystem dirtParticle;
    public AudioClip shootingSound;
    private AudioSource playerAudio;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    private int score;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        score = 0;
        health = 10;
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Keep the player in bounds
        if(transform.position.x < -xRange) {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if(transform.position.x > xRange) {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if(this.isGameActive)
        {
            // Move player left and right
            horizontalInput = Input.GetAxis("Horizontal") * speed / 2;
            verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(new Vector3(horizontalInput, 0, verticalInput));

            // Disable player from spamming the screen with projectiles, limiting to 5 projectiles at a time
            projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            projectileCount = projectiles.Length;
            if (Input.GetKeyDown(KeyCode.Space) && projectileCount < 5)
            {
                // Launch a projectile from the player
                var projectilePrefabPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
                Instantiate(projectilePrefab, projectilePrefabPosition, projectilePrefab.transform.rotation);
                playerAudio.PlayOneShot(shootingSound, 1.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.isGameActive)
        {
            spawnManager.SpawnTriggerEntered();
        }
    }


    public void UpdateScore(int scoreToAdd)
    {
        if (this.isGameActive)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateHealth(int healthToLose)
    {
        if (health > 0)
        {
            health -= healthToLose;
            healthText.text = "Health: " + health;
        } else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
}

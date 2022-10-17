using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     // Sheryl Tania
    private float horizontalInput;
    private float verticalInput;
    public float speed = 25.0f;
    public float xRange = 30;
    public float zRange = -50;
    public GameObject projectilePrefab;
    private Rigidbody playerRb;
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
            Debug.Log("Game Over");
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        // Move player left and right
        horizontalInput = Input.GetAxis("Horizontal") * speed / 2;
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(new Vector3(horizontalInput, 0, verticalInput));

        if (Input.GetKeyDown(KeyCode.Space)) {
            // Launch a projectile from the player
            var projectilePrefabPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
            Instantiate(projectilePrefab, projectilePrefabPosition, projectilePrefab.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner; 
    public GameObject player;
    public GameObject enemyPrefab;
    private PlayerController playerController;
    private float spawnRangeX = 10;
    private float spawnPosZ;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        playerController = player.GetComponent<PlayerController>();
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy() {
        if (playerController.isGameActive)
        {
            spawnPosZ = Random.Range(player.transform.position.z + 100, player.transform.position.z + 150);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 100, spawnPosZ);
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
    }
}

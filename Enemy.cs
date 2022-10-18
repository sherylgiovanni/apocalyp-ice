using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    private float speed = 2000.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    public AudioClip spawnedSound;
    private AudioSource enemyAudio;
    public ParticleSystem rollingParticle;
    public ParticleSystem explosionParticle;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.PlayOneShot(spawnedSound, 0.25f);
        explosionParticle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isGameActive)
        {
            enemyRb.AddForce(Vector3.back * Time.deltaTime * speed);
            rollingParticle.Play();

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }

            // if player misses an enemy, minus one health
            if (transform.position.z < player.transform.position.z)
            {
                playerController.UpdateHealth(1);
                Destroy(gameObject);
            }
        }
    }
}

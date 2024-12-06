using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnScript : MonoBehaviour {
    public GameObject cloudPrefab;  // The cloud prefab to spawn
    public float cloudSpawnRate = 1f;    // Time in seconds between spawns
    private float nextCloudSpawnTime = 0f; // Time when the next cloud will spawn
    public GameObject thunderCloudPrefab;  // The cloud prefab to spawn
    public float thunderCloudSpawnRate = 2f;
    public float nextThunderCloudSpawnTime = 0f;
    public float spawnHeightOffset = 5f; // Distance above the camera to spawn clouds
    public float minXOffset = -20f; // Minimum X offset relative to the camera
    public float maxXOffset = 20f;  // Maximum X offset relative to the camera

    public Transform player;
    public GameObject birdPrefab;
    public float birdSpawnDistance = 10f;
    public float birdSpeed = 10f;
    public float birdSpawnRate = 1f;    // Time in seconds between spawns
    private float nextBirdSpawnTime = 0f; // Time when the next cloud will spawn
    public float maxBirdSpawnHeight = 3f;

    public Sprite[] cloudSprites;


    void Update() {
        // Check if it's time to spawn a new cloud
        if (Time.time >= nextCloudSpawnTime) {
            SpawnCloud();
            nextCloudSpawnTime = Time.time + cloudSpawnRate; // Schedule the next spawn
        }
        if (Time.time >= nextThunderCloudSpawnTime) {
            //SpawnThunderCloud();
            nextThunderCloudSpawnTime = Time.time + thunderCloudSpawnRate; // Schedule the next spawn
        }
        if (Time.time >= nextBirdSpawnTime) {
            SpawnBird();
            nextBirdSpawnTime = Time.time + birdSpawnRate; // Schedule the next spawn
        }
    }

    void SpawnCloud() {
        // Determine the spawn position
        float xPosition = Random.Range(player.position.x + minXOffset, player.position.x + maxXOffset);
        float yPosition = player.position.y + spawnHeightOffset;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);

        // Spawn the cloud
        GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
        Sprite selectedSprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
        cloud.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    void SpawnThunderCloud() {
        float xPosition = Random.Range(player.position.x + minXOffset, player.position.x + maxXOffset);
        float yPosition = player.position.y + spawnHeightOffset;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);

        // Spawn the cloud
        GameObject cloud = Instantiate(thunderCloudPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnBird() {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector3 spawnPosition = player.position + new Vector3(direction * birdSpawnDistance, Random.Range(0, maxBirdSpawnHeight), 0);
        GameObject bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
        Vector2 moveDirection = (player.position - bird.transform.position).normalized * birdSpeed;
        rb.velocity = moveDirection;
        if (direction == -1) {
            rb.transform.localScale = new Vector3(-rb.transform.localScale.x, rb.transform.localScale.y, rb.transform.localScale.z);
        }
    }
}

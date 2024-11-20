using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnScript : MonoBehaviour
{
    public GameObject cloudPrefab;  // The cloud prefab to spawn
    public float spawnRate = 2f;    // Time in seconds between spawns
    public float spawnHeightOffset = 5f; // Distance above the camera to spawn clouds
    public float minXOffset = -10f; // Minimum X offset relative to the camera
    public float maxXOffset = 10f;  // Maximum X offset relative to the camera

    private float nextSpawnTime = 0f; // Time when the next cloud will spawn

    void Update() {
        // Check if it's time to spawn a new cloud
        if (Time.time >= nextSpawnTime) {
            SpawnCloud();
            nextSpawnTime = Time.time + spawnRate; // Schedule the next spawn
        }
    }

    void SpawnCloud() {
        if (cloudPrefab == null) {
            Debug.LogError("Cloud prefab is not assigned!");
            return;
        }

        // Determine the spawn position
        float xPosition = Random.Range(Camera.main.transform.position.x + minXOffset, Camera.main.transform.position.x + maxXOffset);
        float yPosition = Camera.main.transform.position.y + spawnHeightOffset;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);

        // Spawn the cloud
        Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
    }
}

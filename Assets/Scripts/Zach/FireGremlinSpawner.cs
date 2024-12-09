using UnityEngine;

public class FireGremlinSpawner : MonoBehaviour
{
    public GameObject fireGremlinPrefab; // Reference to the Fire Gremlin prefab
    public Transform[] spawnPoints; // Array of spawn locations
    public Transform player; // Reference to the player
    public float spawnInterval = 5f; // Time between spawns

    private void Start()
    {
        // Start spawning at regular intervals
        InvokeRepeating(nameof(SpawnFireGremlin), 2f, spawnInterval);
    }

    void SpawnFireGremlin()
    {
        if (fireGremlinPrefab != null && spawnPoints.Length > 0)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate a Fire Gremlin at the selected spawn point
            GameObject fireGremlin = Instantiate(fireGremlinPrefab, spawnPoint.position, Quaternion.identity);

            // Assign the player's Transform to the Fire Gremlin's AI script
            FireGremlinAI gremlinAI = fireGremlin.GetComponent<FireGremlinAI>();
            if (gremlinAI != null)
            {
                gremlinAI.player = player;
            }
        }
        else
        {
            Debug.LogWarning("Fire Gremlin prefab or spawn points not set.");
        }
    }
}

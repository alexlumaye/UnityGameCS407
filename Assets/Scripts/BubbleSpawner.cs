using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnArea;
    public float spawnInterval = 0.5f;
    public Vector2 spawnRange = new Vector2(-5, 5);

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        float randomX = Random.Range(spawnRange.x, spawnRange.y);
        Vector3 spawnPosition = new Vector3(randomX, spawnArea.position.y, 0);
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        Destroy(bubble, 3f); // Destroy after 3 seconds to clean up
    }
}


using UnityEngine;

public class AirBubbleSpawner : MonoBehaviour
{
    public GameObject airBubblePrefab;
    public float spawnInterval = 5f;
    public Vector2 spawnArea;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAirBubble), spawnInterval, spawnInterval);
    }

    private void SpawnAirBubble()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            0
        );
        Instantiate(airBubblePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Air bubble spawned!");
    }
}

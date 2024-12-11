using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxOxygen = 100;
    public int currentOxygen = 100;
    public Vector2 respawnPosition; // Store checkpoint position

    private void Start()
    {
        respawnPosition = transform.position; // Set the initial respawn point
    }

    private void Update()
    {
        // Continuously deplete oxygen over time or based on game logic
        UpdateOxygen();
    }

    private void UpdateOxygen()
    {
        if (currentOxygen > 0)
        {
            currentOxygen -= 1; // Example: Decrease oxygen every frame
            Debug.Log($"Current Oxygen: {currentOxygen}");
        }

        if (currentOxygen <= 0)
        {
            currentOxygen = 0; // Clamp to zero
            Debug.Log("Oxygen depleted!");
        }
    }

 
    public void SetCheckpoint(Vector2 checkpointPosition)
    {
        respawnPosition = checkpointPosition; // Store the new checkpoint position
        Debug.Log($"Checkpoint set to: {checkpointPosition}");
    }

}

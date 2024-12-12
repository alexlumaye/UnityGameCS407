using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public float oxygenLevel = 100f;
    public float oxygenDecayRate = 5f; // Oxygen depletion per second
    public float maxOxygen = 100f;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on the GameObject!");
        }
    }

    void Update()
    {
        if (playerMovement == null) return;

        // Decrease oxygen over time if the player is underwater
        if (playerMovement.IsUnderwater())
        {
            oxygenLevel -= oxygenDecayRate * Time.deltaTime;
            oxygenLevel = Mathf.Clamp(oxygenLevel, 0, maxOxygen);

            if (oxygenLevel <= 0)
            {
                Debug.Log("Player drowned!");
                HandlePlayerDrowning();
            }
        }
        else
        {
            // Regenerate oxygen if not underwater
            oxygenLevel = Mathf.Min(oxygenLevel + 10f * Time.deltaTime, maxOxygen);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AirBubble"))
        {
            IncreaseOxygen(20f);
            Destroy(collision.gameObject);
        }
    }

    public void IncreaseOxygen(float amount)
    {
        oxygenLevel = Mathf.Min(oxygenLevel + amount, maxOxygen);
        Debug.Log($"Oxygen increased by {amount}. Current Oxygen: {oxygenLevel}");
    }

    public void ResetOxygen()
    {
        oxygenLevel = maxOxygen;
        Debug.Log("Oxygen reset to maximum.");
    }

    private void HandlePlayerDrowning()
    {
        Debug.Log("Handle player drowning (e.g., damage or respawn logic).");
        playerMovement.ToggleMovement(); // Stops movement during respawn
        playerMovement.transform.position = new Vector3(0, 0, 0); // Example respawn logic
        ResetOxygen();
        playerMovement.ToggleMovement(); // Restarts movement
    }
}

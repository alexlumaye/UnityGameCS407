using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public float oxygenLevel = 100f;
    public float oxygenDecayRate = 5f; // Oxygen depletion per second
    public float maxOxygen = 100f;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }
    }

    void Update()
    {
        if (playerController == null) return;

        oxygenLevel -= oxygenDecayRate * Time.deltaTime;
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0, maxOxygen);

        if (oxygenLevel <= 0)
        {
            Debug.Log("Player Drowned!");
            playerController.TakeDamage(5);
            playerController.Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AirBubble"))
        {
            oxygenLevel = Mathf.Min(oxygenLevel + 20f, maxOxygen);
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
}

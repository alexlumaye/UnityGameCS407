using UnityEngine;

public class AirBubble : MonoBehaviour
{
    public float maxOxygenIncrease = 30f; // Max oxygen increase possible
    public float minOxygenIncrease = 10f; // Min oxygen increase possible
    public GameObject particleEffectPrefab; // Reference to the particle effect prefab
    private OxygenManager oxygenManager;

    private void Start()
    {
        // Find the OxygenManager in the scene
        oxygenManager = FindObjectOfType<OxygenManager>();
        if (oxygenManager == null)
        {
            Debug.LogError("No OxygenManager found in the scene!");
        }
    }

    private void OnMouseDown()
    {
        if (oxygenManager != null)
        {
            // Randomize oxygen increase
            float oxygenIncrease = Random.Range(minOxygenIncrease, maxOxygenIncrease);
            oxygenManager.IncreaseOxygen(oxygenIncrease);
        }

        // Trigger particle effect
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // Destroy the air bubble after it's clicked
    }
}

using UnityEngine;

public class AirBubble : MonoBehaviour
{
    public float maxOxygenIncrease = 30f; 
    public float minOxygenIncrease = 10f;
    public GameObject particleEffectPrefab; 
    private OxygenManager oxygenManager;

    private void Start()
    {
        oxygenManager = FindObjectOfType<OxygenManager>();
        if (oxygenManager == null)
        {
            Debug.LogError("No OxygenManager found in the scene!");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("bubble");
        if (oxygenManager != null)
        {
            float oxygenIncrease = Random.Range(minOxygenIncrease, maxOxygenIncrease);
            oxygenManager.IncreaseOxygen(oxygenIncrease);
        }

        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); 
    }
}

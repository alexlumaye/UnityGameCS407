using UnityEngine;

public class ScubaMask : MonoBehaviour
{
    public GameObject barricade; // Assign barricade in Inspector
    public string scubaMaskName = "ScubaMask"; // Name of the scuba mask in inventory
    public int maskCount = 1; // Number of scuba masks added to inventory

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the scuba mask to the player's inventory
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddToInventory(scubaMaskName, maskCount);

                // If scuba mask added, destroy the barricade
                if (playerInventory.HasCollectible(scubaMaskName, maskCount))
                {
                    Destroy(barricade);
                    Destroy(gameObject); // Destroy the scuba mask
                    Debug.Log("Scuba mask collected, barricade removed!");
                }
            }
        }
    }
}

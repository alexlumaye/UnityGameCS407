using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;          // Name of the item
    public int value = 1;            // Value of the collectible
    public Sprite itemSprite;        // Sprite representing the item

    private void OnValidate()
    {
        // Ensure the object has a trigger collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        Debug.Log($"{player.name} collected: {itemName}");

        // Attempt to add the item to the inventory
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            bool added = inventoryManager.AddItem(itemSprite);

            if (added)
            {
                Debug.Log($"{itemName} added to inventory.");
                // Notify the CombineManager if necessary
                CombineManager combineManager = FindObjectOfType<CombineManager>();
                if (combineManager != null)
                {
                    combineManager.AddItem(itemName);
                }

                Destroy(gameObject); // Remove the collectible from the scene
            }
            else
            {
                Debug.LogWarning("Inventory is full. Cannot collect this item.");
            }
        }
        else
        {
            Debug.LogError("InventoryManager not found in the scene.");
        }
    }
}

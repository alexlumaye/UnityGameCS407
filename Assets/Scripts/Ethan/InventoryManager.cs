using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this for scene management

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the inventory UI panel
    public List<Image> slotImages;   // List of UI Images for inventory slots (6 slots)
    public List<Sprite> items = new List<Sprite>(); // List of collected item sprites

    private const int MaxItems = 6; // Define the maximum number of items allowed

    private void Start()
    {
        // Ensure all slots are initially empty
        foreach (Image slot in slotImages)
        {
            ClearSlot(slot);
        }
    }

    public bool AddItem(Sprite itemSprite)
    {
        if (items.Count < MaxItems) // Ensure inventory is not full
        {
            Debug.Log(items.Count);
            Debug.Log(MaxItems);
            items.Add(itemSprite);

            // Update the UI to display the collected item
            UpdateInventoryUI();

            Debug.Log($"Item added to inventory! Current count: {items.Count}");

            // Check if the inventory is full
            if (items.Count == MaxItems)
            {
                Debug.Log("Inventory full! Loading next scene...");
                LoadNextScene();
            }

            return true;
        }

        Debug.Log("Inventory is full!");
        return false;
    }

    public bool IsInventoryFull()
    {
        return items.Count >= MaxItems;
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slotImages.Count; i++)
        {
            if (i < items.Count)
            {
                slotImages[i].sprite = items[i];
                slotImages[i].color = new Color(1, 1, 1, 1); // Make slot visible
            }
            else
            {
                ClearSlot(slotImages[i]);
            }
        }
    }

    private void ClearSlot(Image slot)
    {
        slot.sprite = null;
        slot.color = new Color(1, 1, 1, 0); // Make slot transparent
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("ErikScene");
    }
}

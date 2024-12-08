using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour {
    PlayerInventory playerInventory;
    TextMeshProUGUI tableText;

    void Start() {
        playerInventory = FindObjectOfType<PlayerInventory>();
        tableText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        tableText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        int numWood = playerInventory.CheckCollectibleCount("Wood");
        bool hasSword = playerInventory.HasCollectible("Wooden_Sword");
        bool hasShield = playerInventory.HasCollectible("Wooden_Shield");
        bool hasSledgehammer = playerInventory.HasCollectible("Wooden_Sledgehammer");

        if (!hasSword && numWood < 10) tableText.text = "Collect 10 wood to craft a wooden sword!";
        else if (!hasSword) {
            tableText.text = "You crafted a wooden sword! You can now attack enemies!";
            playerInventory.AddToInventory("Wooden_Sword", 1);
            playerInventory.RemoveFromInventory("Wood", 10);
        } else if (!hasShield && numWood < 5) tableText.text = "Collect 5 wood to craft a wooden shield!";
        else if (!hasShield) {
            tableText.text = "You crafted a wooden shield! You will no longer get one shot!";
            playerInventory.AddToInventory("Wooden_Shield", 1);
            playerInventory.RemoveFromInventory("Wood", 5);
        } else if (!hasSledgehammer && numWood < 3) tableText.text = "Collect 3 wood to craft a wooden shield!";
        else if (!hasSledgehammer) {
            playerInventory.AddToInventory("Wooden_Sledgehammer", 1);
            playerInventory.RemoveFromInventory("Wood", 3);
        } else {
            tableText.text = "You cannot craft anything else here.";
        }

        tableText.enabled = true;

        Debug.Log("Has Sword: " + hasSword + "\nHas Shield: " + hasShield);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        tableText.enabled = false;
    }
}

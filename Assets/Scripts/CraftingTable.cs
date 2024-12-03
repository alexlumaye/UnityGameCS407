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

        if (!hasSword && numWood < 10) tableText.text = "Collect 10 wood to craft a wooden sword!";
        else if (!hasSword) {
            tableText.text = "You crafted a wooden sword!";
            playerInventory.AddToInventory("Wooden_Sword", 1);
        }
        else if (!hasShield && numWood < 5) tableText.text = "Collect 5 wood to craft a wooden shield!";
        else if (!hasShield) {
            tableText.text = "You crafted a wooden shield!";
            playerInventory.AddToInventory("Wooden_Shield", 1);
        } else {
            tableText.text = "You cannot craft anything else here.";
        }

        tableText.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        tableText.enabled = false;
    }
}

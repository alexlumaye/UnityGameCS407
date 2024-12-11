using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory : MonoBehaviour {
    private Dictionary<string, int> inventory = new();
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void AddToInventory(GameObject collectible, int count) {
        if (inventory.ContainsKey(collectible.name)) {
            inventory[collectible.name] += count;
        } else {
            inventory.Add(collectible.name, count);
        }
    }

    public bool RemoveFromInventory(GameObject collectible, int requiredCount) {
        if (inventory.ContainsKey(collectible.name)) return false;

        int collectibleCount = inventory[collectible.name];

        if (collectibleCount < requiredCount) return false;

        inventory[collectible.name] -= requiredCount;

        return true;
    }

    public void ClearCollectible(GameObject collectible) {
        inventory.Remove(collectible.name);
    }

    public void ClearInventory() {
        inventory.Clear();
    }

    public bool HasCollectible(GameObject collectible, int requiredCount = 1) {
        if (!inventory.ContainsKey(collectible.name)) return false;
        return inventory[collectible.name] >= requiredCount;
    }
}

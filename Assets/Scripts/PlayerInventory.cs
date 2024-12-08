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

    public void AddToInventory(string collectibleName, int count) {
        if (inventory.ContainsKey(collectibleName)) {
            inventory[collectibleName] += count;
        } else {
            inventory.Add(collectibleName, count);
        }
    }

    public bool RemoveFromInventory(string collectibleName, int requiredCount) {
        if (!inventory.ContainsKey(collectibleName)) return false;

        int collectibleCount = inventory[collectibleName];

        if (collectibleCount < requiredCount) return false;

        inventory[collectibleName] -= requiredCount;

        return true;
    }

    public void ClearCollectible(string collectibleName) {
        inventory.Remove(collectibleName);
    }

    public void ClearInventory() {
        inventory.Clear();
    }

    public bool HasCollectible(string collectibleName, int requiredCount = 1) {
        if (!inventory.ContainsKey(collectibleName)) return false;
        return inventory[collectibleName] >= requiredCount;
    }

    public int CheckCollectibleCount(string collectibleName) {
        return inventory.ContainsKey(collectibleName) ? inventory[collectibleName] : 0;
    }
}

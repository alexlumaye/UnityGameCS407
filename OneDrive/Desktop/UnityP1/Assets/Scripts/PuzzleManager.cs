
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private Dictionary<string, int> collectedItems = new Dictionary<string, int>();
    public GameObject wings;

    public void CollectItem(string itemName)
    {
        if (!collectedItems.ContainsKey(itemName))
        {
            collectedItems[itemName] = 0;
        }
        collectedItems[itemName]++;

        if (CheckIfPuzzleComplete())
        {
            CombineItems();
        }
    }

    bool CheckIfPuzzleComplete()
    {
        return collectedItems.ContainsKey("Shell") && collectedItems["Shell"] >= 2 &&
               collectedItems.ContainsKey("Seaweed") && collectedItems["Seaweed"] >= 1;
    }

    void CombineItems()
    {
        Debug.Log("Puzzle Complete! Wings Created!");
        Instantiate(wings, transform.position, Quaternion.identity);
        collectedItems.Clear();
    }
}

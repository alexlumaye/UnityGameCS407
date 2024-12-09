
using UnityEngine;

public class CombineManager : MonoBehaviour
{
    public GameObject makeshiftWingsPrefab;
    private int seaweedCount = 0;
    private int shellCount = 0;

    public void AddItem(string itemName)
    {
        if (itemName == "Seaweed")
            seaweedCount++;
        else if (itemName == "Shell")
            shellCount++;

        CheckForCombination();
    }

    private void CheckForCombination()
    {
        if (seaweedCount >= 2 && shellCount >= 1)
        {
            CreateWings();
            seaweedCount -= 2;
            shellCount -= 1;
        }
    }

    private void CreateWings()
    {
        Instantiate(makeshiftWingsPrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("Makeshift wings created!");
    }
}


using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public string itemName;

    void OnMouseDown()
    {
        Debug.Log($"Collected {itemName}");
        FindObjectOfType<PuzzleManager>().CollectItem(itemName);
        Destroy(gameObject);
    }
}

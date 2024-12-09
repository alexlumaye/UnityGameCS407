
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        Debug.Log(player.name + " collected: " + itemName);
        Destroy(gameObject);
    }
}

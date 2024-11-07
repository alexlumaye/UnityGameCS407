using UnityEngine;

public class Collectible : MonoBehaviour {
    public int value = 1;

    void OnValidate() {
        Collider2D collider = GetComponent<Collider2D>();

        if (collider == null) collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    /**
     What should happen if a player comes in contact with a collectible.
    */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject); // Destroy the collectible.
            other.GetComponent<PlayerInventory>().AddToInventory(gameObject, value);
        }
    }
}

using UnityEngine;

public class Collectible : MonoBehaviour {
    public int value = 1;

    void OnValidate() {
        Collider2D collider = GetComponent<Collider2D>();

        if (collider == null) collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject); 
            other.GetComponent<PlayerInventory>().AddToInventory(gameObject, value);
        }
    }
}

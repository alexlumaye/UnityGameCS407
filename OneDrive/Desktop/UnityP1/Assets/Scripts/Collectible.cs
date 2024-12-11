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
<<<<<<<< HEAD:Assets/Scripts/Alex/Collectible.cs
            Destroy(gameObject); // Destroy the collectible.
            other.GetComponent<PlayerInventory>().AddToInventory(gameObject.name, value);
========
            Destroy(gameObject); 
            other.GetComponent<PlayerInventory>().AddToInventory(gameObject, value);
>>>>>>>> water:OneDrive/Desktop/UnityP1/Assets/Scripts/Collectible.cs
        }
    }
}

using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player player = collision.GetComponent<Player>();
            if (player != null) {
                player.SetCheckpoint(transform.position); // Update the player's checkpoint
                Debug.Log("Checkpoint activated at: " + transform.position);
            }
        }
    }
}

using UnityEngine;

public class Checkpoint2 : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player player = collision.GetComponent<Player>();
            if (player != null) {
                player.SetCheckpoint(transform.position); 
                Debug.Log("Checkpoint activated at: " + transform.position);
            }
        }
    }
}
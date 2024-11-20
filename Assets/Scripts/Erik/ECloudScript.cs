using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECloudScript : MonoBehaviour
{
    public float minXScale = 0.5f; // Minimum X scale
    public float maxXScale = 2.0f; // Maximum X scale

    public float minGravityScale = 0.5f; // Minimum gravity scale
    public float maxGravityScale = 3.0f; // Maximum gravity scale

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start() {
        // Randomize the X scale
        float randomXScale = Random.Range(minXScale, maxXScale);
        transform.localScale = new Vector3(randomXScale, transform.localScale.y, transform.localScale.z);

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            // Randomize the gravity scale
            rb.gravityScale = Random.Range(minGravityScale, maxGravityScale);
        } else {
            Debug.LogWarning("No Rigidbody2D found on the GameObject!");
        }
    }

    private void Update() {
        if (transform.position.y < -10f) {
            Destroy(gameObject);
        }
    }
}

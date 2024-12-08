using UnityEngine;

public class BackgroundFollow2D : MonoBehaviour {
    public Transform player; // Reference to the player
    public float maxSpeed = 5f; // Maximum speed of the background
    public float minSpeed = 1f; // Minimum speed of the background
    public float distanceThreshold = 5f; // Distance at which speed transitions between min and max
    public float upwardSpeed = 1;

    private Vector2 backgroundCenter;

    void Start() {
        // Assuming the background's center is its initial position
        backgroundCenter = new Vector2(transform.position.x, transform.position.y);
    }

    void Update() {
        // Calculate the distance between the player and the center of the background
        float distance = Vector2.Distance(player.position, backgroundCenter);

        // Calculate the speed based on the distance
        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / distanceThreshold);

        // Calculate the target position (only x and y, keeping z unchanged)
        Vector2 targetPosition = Vector2.Lerp(
            new Vector2(transform.position.x, transform.position.y),
            player.position,
            speed * Time.deltaTime
        );

        // Prevent the background from moving down
        float newY = Mathf.Max(targetPosition.y, transform.position.y) + (Vector3.up * upwardSpeed * Time.deltaTime).y;

        // Update the background position (preserve the z position)
        transform.position = new Vector3(targetPosition.x, newY, transform.position.z);
    }
}

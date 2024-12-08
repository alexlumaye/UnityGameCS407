using UnityEngine;

public class EPlayerScript : MonoBehaviour {
    public float flySpeed = 5f; // Base speed
    public float horizontalSpeed = 3f; // Horizontal speed
    public float maxSpeed = 10f; // Maximum velocity limit
    public float smoothAcceleration = 5f; // Smoothing factor for acceleration

    private Rigidbody2D playerRigidbody;
    private Vector2 targetVelocity;

    public int score = 0;
    public int health = 3;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Get input
        float horizontalInput = Input.acceleration.x;
        if (horizontalInput == 0) {
            horizontalInput = Input.GetAxis("Horizontal");
        }

            // Set target velocity
            targetVelocity = new Vector2(horizontalInput * horizontalSpeed, 0);

        // Add upward thrust when pressing space
        if (Input.GetKey(KeyCode.Space)) {
            targetVelocity.y += flySpeed;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            targetVelocity.y += flySpeed * 2;
        }

        score += Mathf.CeilToInt(1 * Time.deltaTime);

        // End game when player moves off screen

        float bottomBoundary = Camera.main.transform.position.y - Camera.main.orthographicSize;

        if (playerRigidbody.position.y < bottomBoundary) {
            damage(health);
        }
    }

    void FixedUpdate() {
        // Smoothly adjust the velocity
        Vector2 currentVelocity = playerRigidbody.velocity;
        Vector2 smoothedVelocity = Vector2.Lerp(currentVelocity, targetVelocity, smoothAcceleration * Time.fixedDeltaTime);

        // Clamp to max speed
        smoothedVelocity = Vector2.ClampMagnitude(smoothedVelocity, maxSpeed);

        // Apply velocity
        playerRigidbody.velocity = smoothedVelocity;
    }

    public void damage(int amount = 1) {
        health -= amount;
    }
}

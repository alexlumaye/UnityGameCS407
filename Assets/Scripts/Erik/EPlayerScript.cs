using UnityEngine;

public class EPlayerScript : MonoBehaviour {
    public float flySpeed = 5f; // Base speed
    public float horizontalSpeed = 3f; // Horizontal speed
    public float maxSpeed = 10f; // Maximum velocity limit
    public float smoothAcceleration = 5f; // Smoothing factor for acceleration

    private Rigidbody2D playerRigidbody;
    private Vector2 targetVelocity;

    public int score = 0;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set target velocity
        targetVelocity = new Vector2(horizontalInput * horizontalSpeed, verticalInput * flySpeed);

        // Add upward thrust when pressing space
        if (Input.GetKey(KeyCode.Space)) {
            targetVelocity.y += flySpeed;
        }

        score += 1;
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
}

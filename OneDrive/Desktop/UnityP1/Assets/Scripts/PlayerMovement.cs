using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float walkspeed, swimspeed, flyspeed, jumpforce;
    public bool canFly = false;
    private LayerMask groundLayer, waterLayer; // Layer mask for the ground
    private Collider2D playerCollider; // Collider element of the object
    private Rigidbody2D playerRigidbody; // Rigidbody element of the object
    private float lastHoldingJumpBoost;
    private bool isGrounded, isSwimming, isTouchingWater;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        waterLayer = 1 << LayerMask.NameToLayer("Water");
        playerRigidbody.gravityScale = 6;
    }

    void Update() {
        CheckEnvironment();

        // Get horizontal input
        float horizontalVelocity;
        float verticalVelocity;

        // If walking
        if (isSwimming) {
            horizontalVelocity = Input.GetAxis("Horizontal") * swimspeed;
            verticalVelocity = Input.GetAxis("Vertical") * swimspeed * 0.75f;
        } else if (canFly) {
            horizontalVelocity = Input.GetAxis("Horizontal") * flyspeed;
            verticalVelocity = Input.GetAxis("Vertical") * flyspeed;
        } else {
            if (!isTouchingWater) horizontalVelocity = Input.GetAxis("Horizontal") * walkspeed;
            else horizontalVelocity = Input.GetAxis("Horizontal") * (walkspeed + swimspeed) / 2;
            verticalVelocity = playerRigidbody.linearVelocity.y;

            // If grounded and jump is pressed, apply force
            if (isGrounded && Input.GetKey(KeyCode.Space)) {
                verticalVelocity += jumpforce;
                lastHoldingJumpBoost = Time.time + 0.1f;
            }

            // Handle holding space for slightly bigger jumps
            if (!isGrounded && verticalVelocity > 0 && Time.time - lastHoldingJumpBoost >= 0.03f && Input.GetKey(KeyCode.Space)) {
                verticalVelocity *= 1.07f;
                lastHoldingJumpBoost = Time.time;
            }
        }

        // Move the character
        playerRigidbody.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void CheckEnvironment() {
        Vector2 playerPos = (Vector2)transform.position;
        Vector2 playerFeet = playerPos - new Vector2(0, playerCollider.bounds.extents.y);
        Vector2 playerHead = playerPos + new Vector2(0, playerCollider.bounds.extents.y);
        Vector2 playerBox = new(playerCollider.bounds.size.x, playerCollider.bounds.size.y);

        isGrounded = Physics2D.Raycast(playerFeet, Vector2.down, 0.1f, groundLayer) && playerRigidbody.linearVelocity.y == 0;
        isTouchingWater = Physics2D.BoxCast(playerPos, playerBox, 0, Vector2.zero, 0f, waterLayer);
        isSwimming = Physics2D.Raycast(playerPos, Vector2.up, 0.1f, waterLayer);
    }

    public bool IsUnderwater() {
        return isSwimming;
    }

    public void ToggleFlight() {
        canFly = !canFly;

        Physics2D.gravity = canFly ? Vector3.zero : new Vector3(0f, -9.81f, 0f);
    }
}

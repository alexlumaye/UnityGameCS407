using System.Collections;
using UnityEngine;

public class EPlayerScript : MonoBehaviour {
    public float flyForce = 10f; // Upward force applied when pressing space
    public float horizontalSpeed = 3f; // Horizontal speed
    public float maxSpeed = 10f; // Maximum velocity limit
    public float smoothAcceleration = 5f; // Smoothing factor for horizontal motion

    private Rigidbody2D playerRigidbody;
    private Vector2 targetVelocity;

    public int score = 0;
    public int health = 3;

    public SpriteRenderer spriteRenderer;
    public Sprite flyUpSprite;
    public Sprite flyDownSprite;
    private bool isUsingFlyUpSprite = true;
    private float spriteSwitchCooldown = 0.5f;
    private float nextSpriteSwitchTime = 0f;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float horizontalInput = Input.acceleration.x * 3;
        if (horizontalInput == 0) {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        targetVelocity = new Vector2(horizontalInput * horizontalSpeed, playerRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            ApplyUpwardForce();
            SwitchSprite();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            ApplyUpwardForce();
            SwitchSprite();
        }

        score += Mathf.CeilToInt(1 * Time.deltaTime);

        float bottomBoundary = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (playerRigidbody.position.y < bottomBoundary) {
            damage(health);
        }
    }

    void FixedUpdate() {
        Vector2 currentVelocity = playerRigidbody.velocity;
        Vector2 smoothedVelocity = Vector2.Lerp(
            currentVelocity,
            new Vector2(targetVelocity.x, currentVelocity.y), // Keep vertical velocity
            smoothAcceleration * Time.fixedDeltaTime
        );

        smoothedVelocity.x = Mathf.Clamp(smoothedVelocity.x, -maxSpeed, maxSpeed);

        playerRigidbody.velocity = new Vector2(smoothedVelocity.x, playerRigidbody.velocity.y);
    }

    private void ApplyUpwardForce() {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0); // Reset vertical velocity
        playerRigidbody.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse); // Add upward force
    }

    public void damage(int amount = 1) {
        health -= amount;
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed() {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = originalColor;
    }

    private void SwitchSprite() {
        if (Time.time < nextSpriteSwitchTime) {
            return;
        }

        spriteRenderer.sprite = isUsingFlyUpSprite ? flyDownSprite : flyUpSprite;
        isUsingFlyUpSprite = !isUsingFlyUpSprite;

        nextSpriteSwitchTime = Time.time + spriteSwitchCooldown;
    }
}

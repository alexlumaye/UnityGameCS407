
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float oxygenLevel = 100f;
    public float oxygenDecayRate = 2f;
    public GameObject airBubblePrefab;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Player movement input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Oxygen management
        oxygenLevel -= oxygenDecayRate * Time.deltaTime;
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0, 100);

        if (oxygenLevel <= 0)
        {
            Debug.Log("Player has run out of oxygen!");
            Die();
        }
    }

    private void FixedUpdate()
    {
        // Apply movement
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AirBubble"))
        {
            CollectAirBubble(collision.gameObject);
        }
    }

    private void CollectAirBubble(GameObject airBubble)
    {
        oxygenLevel += 20f;  // Replenish oxygen
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0, 100);
        Destroy(airBubble);
        Debug.Log("Collected air bubble! Oxygen level: " + oxygenLevel);
    }

    private void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has drowned!");
    }
}

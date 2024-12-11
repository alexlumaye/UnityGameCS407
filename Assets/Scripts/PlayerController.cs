using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public int maxLives = 5; // Maximum lives
    private int currentLives; // Current lives

    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector2 respawnPosition;

    private HealthManager healthManager; 
    private InventoryManager inventoryManager; 
    private OxygenManager oxygenManager; 

    public Joystick joystick; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;

        respawnPosition = transform.position;

        healthManager = FindObjectOfType<HealthManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        oxygenManager = FindObjectOfType<OxygenManager>();

        if (healthManager != null)
        {
            HealthManager.health = currentLives; 
        }

        if (oxygenManager == null)
        {
            Debug.LogError("OxygenManager not found in the scene!");
        }
    }

    private void Update()
    {
        // Get input from the joystick
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1); // Lose a life if hit by an enemy
        }
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage; // Reduce lives by the damage value
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        Debug.Log($"Player hit! Lives remaining: {currentLives}");

        StartCoroutine(GetHurt()); // Trigger the GetHurt coroutine

        if (healthManager != null)
        {
            HealthManager.health = currentLives;
        }

        if (currentLives <= 0)
        {
            Respawn(); // Respawn only when all lives are lost
        }
    }

    public void Respawn()
    {
        Debug.Log("Player respawning...");

        // Reset lives and position
        currentLives = maxLives;
        transform.position = respawnPosition;

        if (oxygenManager != null)
        {
            oxygenManager.ResetOxygen(); // Reset oxygen using OxygenManager
        }

        Debug.Log($"Player respawned at: {respawnPosition} with full health and oxygen.");
    }

    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}

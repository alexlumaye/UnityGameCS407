using UnityEngine;

public class FireGremlinAI : MonoBehaviour
{
    public float speed = 5f;  // Movement speed (increase to see faster movement)
    public float attackRange = 2f;
    public float damage = 5f;  // Damage dealt to the player
    public int health = 5;  // Health of the Fire Gremlin
    public Transform player;

    private bool isTouchingPlayer = false;  // Flag to track if in contact with player
    private float lastClickTime = 0f;  // Time of the last mouse click
    private float doubleClickTimeLimit = 0.3f;  // Time window for double-click detection
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found! Please attach one to the Fire Gremlin.");
        }
        playerAnimator = FindObjectOfType<Player>().GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        // Move towards the player
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }

        // Detect mouse click on this specific Fire Gremlin
        if (Input.GetMouseButtonDown(0))
        {  // Left mouse button
            HandleDoubleClick();
        }
    }

    private void HandleDoubleClick()
    {
        // Raycast to check if we are clicking on this Fire Gremlin
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // If we hit this specific Fire Gremlin, check for double-click
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTimeLimit)
            {
                playerAnimator.SetTrigger("Attack");
                DestroyGremlin();
            }
            lastClickTime = Time.time;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = player.position;
        Vector3 currentPosition = transform.position;

        // Determine the direction to move in
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // Flip the sprite based on movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true; // Facing right
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = false; // Facing left
        }

        // Move towards the player based on speed
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Continuously deal damage while in contact with the player
        if (isTouchingPlayer && collision.gameObject.CompareTag("Player"))
        {
            AttemptToDamagePlayer();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Stop dealing damage once the player is no longer in contact
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    void AttemptToDamagePlayer()
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            bool damageApplied = playerScript.Damage(damage);
            if (damageApplied)
            {
                Debug.Log("Fire Gremlin consistently dealt " + damage + " damage to the player!");
            }
        }
    }

    void DestroyGremlin()
    {
        Debug.Log("Fire Gremlin has been destroyed!");
        // You can also play a death animation or sound here
        Destroy(gameObject);  // Destroy the gremlin game object
    }
}

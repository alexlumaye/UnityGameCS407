using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public float avoidanceRadius = 1.5f; 
    public int damage = 1; // Amount of damage dealt to the player
    public Transform player;

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }

        AvoidOtherEnemies();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void AvoidOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (Collider2D collider in nearbyEnemies)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector3 directionAway = (transform.position - collider.transform.position).normalized;
                transform.position += directionAway * speed * Time.deltaTime * 0.5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(collision.gameObject);
            Debug.Log("Lost life");

        }
    }

    private void DamagePlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(damage); // Damage the player
        }
    }
}

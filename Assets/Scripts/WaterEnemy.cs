
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public float damage = 10f;
    public Transform player;

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(collision.gameObject);
        }
    }

    private void DamagePlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.oxygenLevel -= damage;
            Debug.Log("Player took damage! Oxygen level: " + playerController.oxygenLevel);
        }
    }
}

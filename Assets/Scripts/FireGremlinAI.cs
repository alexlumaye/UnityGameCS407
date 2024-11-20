using UnityEngine;

public class FireGremlinAI : MonoBehaviour {
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public float damage = 1f;
    public Transform player;

    private float lastAttackTime;

    void Update() {
        if (player == null) return;
        // Move towards the player
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > attackRange) {
            MoveTowardsPlayer();
        } else {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void AttackPlayer() {
        if (Time.time - lastAttackTime >= attackCooldown) {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null) {
                bool damageApplied = playerScript.Damage(damage);
                if (damageApplied) {
                    Debug.Log("Fire Gremlin dealt " + damage + " damage to the player!");
                }
            }
            lastAttackTime = Time.time;
        }
    }
}

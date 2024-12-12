using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public float avoidanceRadius = 1.5f; 
    public int damage = 10; 
    public float minYPosition = 5f; 

    public Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

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

        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, minYPosition), transform.position.z);
    }

    private void AvoidOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (Collider2D collider in nearbyEnemies)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector3 directionAway = (transform.position - collider.transform.position).normalized;
                transform.position += directionAway * speed * Time.deltaTime * 1.5f;
            }
        }

        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, minYPosition), transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
            Debug.Log("Player damaged by enemy.");
        }
    }
   
    private void DamagePlayer()
    {
       Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            bool damageApplied = playerScript.Damage(damage);

        }
    }

}

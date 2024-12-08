using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ECloudScript : MonoBehaviour {
    public float minXScale = 0.5f; // Minimum X scale
    public float maxXScale = 2.0f; // Maximum X scale

    //public float minGravityScale = 0.5f; // Minimum gravity scale
    //public float maxGravityScale = 3.0f; // Maximum gravity scale

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Transform playerTransform;

    private bool isThunder = false;

    public ParticleSystem particleEffectPrefab;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        if (Random.Range(0, 3) == 0) {
            isThunder = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    private void Update() {
        if (transform.position.y < playerTransform.position.y - 10f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (isThunder && collision.gameObject.CompareTag("Player")) {
            particleEffectPrefab.Play();
            EPlayerScript playerScript = collision.gameObject.GetComponent<EPlayerScript>();
            playerScript.damage();
        }
    }
}

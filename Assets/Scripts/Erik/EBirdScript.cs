using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBirdScript : MonoBehaviour
{
    public GameObject bird;

    public ParticleSystem particleEffectPrefab;

    void Start() {
        Destroy(bird, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            particleEffectPrefab.Play();
            EPlayerScript playerScript = collision.gameObject.GetComponent<EPlayerScript>();
            playerScript.damage();
            bird.GetComponent<Renderer>().enabled = false;
        }
    }
}

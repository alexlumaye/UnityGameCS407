using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EColideScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EPlayerScript playerScript = collision.gameObject.GetComponent<EPlayerScript>();
            playerScript.damage();
        }
    }
}

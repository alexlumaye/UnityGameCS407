using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour {

    CameraMovement playerCamera;

    void Start() {
        playerCamera = FindObjectOfType<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerCamera.SetZoomDistanceSmoothly(1.1f);

            Helper.SetTimeout(() => {
                // Teleport player
                SceneManager.LoadScene("waterScene");
            }, 3f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cave : MonoBehaviour {
    PlayerInventory playerInventory;
    TextMeshProUGUI rockTutorialText;
    GameObject rock;
    CameraMovement playerCamera;
    PlayerMovement playerMovement;

    void Start() {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        rockTutorialText = GameObject.Find("Rock_Tutorial").GetComponent<TextMeshProUGUI>();
        rock = GameObject.Find("Cave Rock");
        playerCamera = FindObjectOfType<CameraMovement>();
        rockTutorialText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(rock.IsDestroyed());
        if (!other.CompareTag("Player")) return;

        if (playerInventory.HasCollectible("Wooden_Sledgehammer")) {
            rockTutorialText.text = "You've smashed the rock and can enter the cave now!";
            playerInventory.RemoveFromInventory("Wooden_Sledgehammer", 1);
            Destroy(rock);
        } else if (rock.IsDestroyed()) {
            playerMovement.ToggleMovement();

            playerCamera.SetZoomDistanceSmoothly(1.1f);

            Helper.SetTimeout(() => {
                // Teleport player
                SceneManager.LoadScene("FireScene");
            }, 3f);
        }

        rockTutorialText.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        rockTutorialText.enabled = false;
    }
}

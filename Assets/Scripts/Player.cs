using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float maxHP, maxOxygen, immortalityFrameDuration;
    private float currentHP, currentOxygen, lastOxygenChange, lastDamageTaken;
    private Vector2 checkpoint = new(0, 0);
    private PlayerMovement playerMovement;
    private CameraMovement cameraMovement;
    public bool isImmortal;
    public bool hasScubaMask;
    public GameObject barricade;
    public GameObject[] healthSprites; // Array for health sprites
    private int currentHealthIndex; // Track current index based on player's health

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        currentHP = maxHP;
        currentOxygen = maxOxygen;

        UpdateHealthBar(); // Initialize the health bar UI on game start
    }

    void Update() {
        if (Time.time - lastOxygenChange > 1f) {
            currentOxygen = playerMovement.IsUnderwater()
                ? Math.Max(0, currentOxygen - 1)
                : Math.Min(maxOxygen, currentOxygen + 5);
            lastOxygenChange = Time.time;
        }

        if (currentOxygen == 0) Damage(1);
        if (transform.position.y < -1000) Damage(100);
    }

    public void SetCheckpoint(Vector2 pos) {
        checkpoint = pos;
    }

    /// <summary>
    /// Heals the player by an amount up to the maximum HP.
    /// </summary>
    public void Heal(float amountToHeal) {
        currentHP = Math.Min(maxHP, currentHP + amountToHeal);
        UpdateHealthBar();
    }

    /// <summary>
    /// Handles damage logic, immortality checks, and screen shake effects.
    /// </summary>
    public bool Damage(float amountToDamage, bool bypassImmortalityFrame = false) {
        if (bypassImmortalityFrame || lastDamageTaken - Time.time > 0 || currentHP == 0 || amountToDamage < 1 || isImmortal) return false;

        lastDamageTaken = Time.time + immortalityFrameDuration;
        currentHP = Math.Max(0, currentHP - amountToDamage);
        cameraMovement.ShakeScreen(2, 0.2f);

        if (currentHP == 0) {
            TeleportToCheckpoint();
            currentHP = maxHP;
            UpdateHealthBar();
            Debug.Log("You Died");
        }

        UpdateHealthBar();
        return true;
    }

    public void TeleportToCheckpoint() {
        transform.position = checkpoint;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Lava")) {
            Debug.Log("Player hit the lava!");
            Damage(10);
        } else if (other.CompareTag("Barricade")) {
            Debug.Log("Running barricade portion");
        if (hasScubaMask) {
            Destroy(other.gameObject); // Destroy the barricade
            Debug.Log("Barricade removed!");
        } else {
            Debug.Log("You need a scuba mask to pass!");
        }
        }
    }

    public void CollectScubaMask() {
        hasScubaMask = true;
        Debug.Log("Scuba mask collected!");
            if (barricade != null) {
            Destroy(barricade); // Destroy the barricade when the scuba mask is collected
        } else {
            Debug.LogWarning("Barricade reference is missing!");
        }
    }

    private void UpdateHealthBar() {
        // Directly map current HP to the correct health sprite index
        currentHealthIndex = Mathf.Clamp((int)currentHP - 1, 0, healthSprites.Length - 1);

        for (int i = 0; i < healthSprites.Length; i++) {
            healthSprites[i].SetActive(i == currentHealthIndex);
        }

        Debug.Log($"Health Index: {currentHealthIndex} | Current HP: {currentHP}/{maxHP}");
    }

}

using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxHP, maxOxygen, immortalityFrameDuration;
    private float currentHP, currentOxygen, lastOxygenChange, lastDamageTaken;
    private Vector2 checkpoint = new(0, 0);
    private PlayerMovement playerMovement;
    private CameraMovement cameraMovement;
    public bool isImmortal;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        currentHP = maxHP;
        currentOxygen = maxOxygen;
    }

    void Update() {
        if (Time.time - lastOxygenChange > 1f) {
            currentOxygen = playerMovement.IsUnderwater() ? Math.Max(0, currentOxygen - 1) : Math.Min(maxOxygen, currentOxygen + 5);
            lastOxygenChange = Time.time;
        }

        if (currentOxygen == 0) Damage(1);
        if (transform.position.y < -1000) Damage(100);
    }

    public void SetCheckpoint(Vector2 pos) {
        checkpoint = pos;
    }

    /// <summary>
    ///  Heals the player by an amount up to the maximum HP.
    /// </summary>
    /// <param name="amountToHeal">The amount to heal the player.</param>
    public void Heal(float amountToHeal) {
        currentHP = Math.Min(maxHP, currentHP + amountToHeal);
    }

    /// <summary>
    /// Returns whether or not the damage was applied (depending on immortality or invincibility frames);
    /// </summary>
    /// <param name="amountToDamage">The amount to damage the player, down to 0.</param>
    /// <param name="bypassImmortalityFrame">Whether to bypass the player's immortality frame</param>
    public bool Damage(float amountToDamage, bool bypassImmortalityFrame = false) {
        if (bypassImmortalityFrame || lastDamageTaken - Time.time > 0 || currentHP == 0 || amountToDamage < 1 || isImmortal) return false;

        lastDamageTaken = Time.time + immortalityFrameDuration;
        currentHP = Math.Max(0, currentHP - amountToDamage);
        cameraMovement.ShakeScreen(2, 0.2f);

        if (currentHP == 0) {
            TeleportToCheckpoint();
            currentHP = maxHP;
            Debug.Log("You Died");
        }

        return true;
    }

    public void TeleportToCheckpoint() {
        transform.position = checkpoint;
    }
}
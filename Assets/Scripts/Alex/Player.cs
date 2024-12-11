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
    private Animator playerAnimator;
    private Image healthBar;
    public bool isImmortal;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        playerAnimator = GetComponent<Animator>();
        healthBar = GameObject.Find("Health").GetComponent<Image>();
        healthBar.fillAmount = 1f;


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
        healthBar.fillAmount = currentHP / maxHP;
    }

    /// <summary>
    /// Returns whether or not the damage was applied (depending on immortality or invincibility frames);
    /// </summary>
    /// <param name="amountToDamage">The amount to damage the player, down to 0.</param>
    /// <param name="bypassImmortalityFrame">Whether to bypass the player's immortality frame</param>
    public bool Damage(float amountToDamage, bool bypassImmortalityFrame = false) {
        if ((lastDamageTaken - Time.time > 0 && !bypassImmortalityFrame) || currentHP == 0 || amountToDamage < 1 || isImmortal) return false;

        lastDamageTaken = Time.time + immortalityFrameDuration;
        currentHP = Math.Max(0, currentHP - amountToDamage);
        cameraMovement.ShakeScreen(2, 0.2f);

        if (currentHP == 0) {
            playerAnimator.SetBool("Die", true);
            Physics2D.gravity = Vector3.zero;
            cameraMovement.SetZoomDistanceSmoothly(1.5f);
            playerMovement.ToggleMovement();

            Helper.SetTimeout(() => {
                TeleportToCheckpoint();
                playerAnimator.SetBool("Die", false);
                Physics2D.gravity = new Vector3(0f, -9.81f, 0f);
                cameraMovement.SetZoomDistance(10f);
                currentHP = maxHP;
                healthBar.fillAmount = currentHP;
                playerMovement.ToggleMovement();
            }, 5f);
        } else playerAnimator.SetTrigger("Hurt");

        healthBar.fillAmount = currentHP / maxHP;
        return true;
    }

    public void TeleportToCheckpoint() {
        transform.position = checkpoint;
    }
}
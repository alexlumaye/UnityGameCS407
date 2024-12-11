using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour {
    public float walkspeed, swimspeed, flyspeed, jumpforce;
    public bool canFly = false;
    private LayerMask groundLayer, waterLayer; // Layer mask for the ground
    private Collider2D playerCollider; // Collider element of the object
    private Rigidbody2D playerRigidbody; // Rigidbody element of the object
    private float lastHoldingJumpBoost;
    private bool isGrounded, isSwimming, isTouchingWater, stopMovement;
    private Animator playerAnimator;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;


    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        waterLayer = 1 << LayerMask.NameToLayer("Water");
        playerRigidbody.gravityScale = 6;
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        CheckEnvironment();

        // Get horizontal input
        Vector2 inputValue = moveAction.action.ReadValue<Vector2>();
        bool isJumpPressed = jumpAction.action.inProgress;
        float horizontalVelocity = inputValue.x + Input.GetAxis("Horizontal");
        float verticalVelocity = inputValue.y + Input.GetAxis("Vertical");

        // Rotation
        if (horizontalVelocity != 0) playerRigidbody.transform.eulerAngles = new Vector3(0, horizontalVelocity > 0 ? 180 : 0, 0); // Rotate 180 degrees on the Y-axis

        // If walking
        if (isSwimming) {
            horizontalVelocity *= swimspeed;
            verticalVelocity *= swimspeed * 0.75f;
        } else if (canFly) {
            horizontalVelocity *= flyspeed;
            verticalVelocity *= flyspeed;
        } else {
<<<<<<<< HEAD:Assets/Scripts/Alex/PlayerMovement.cs
            if (isTouchingWater) horizontalVelocity *= (walkspeed + swimspeed) / 2;
            else horizontalVelocity *= walkspeed;

            // Remove vertical component while walking normally. Will use jump button instead.
            verticalVelocity = playerRigidbody.velocity.y;
========
            if (!isTouchingWater) horizontalVelocity = Input.GetAxis("Horizontal") * walkspeed;
            else horizontalVelocity = Input.GetAxis("Horizontal") * (walkspeed + swimspeed) / 2;
            verticalVelocity = playerRigidbody.linearVelocity.y;
>>>>>>>> water:OneDrive/Desktop/UnityP1/Assets/Scripts/PlayerMovement.cs

            // If grounded and jump is pressed, apply force
            if (isGrounded && isJumpPressed) {
                verticalVelocity += jumpforce;
                lastHoldingJumpBoost = Time.time + 0.1f;
            }

            // Handle holding space for slightly bigger jumps
            if (!isGrounded && verticalVelocity > 0 && Time.time - lastHoldingJumpBoost >= 0.03f && isJumpPressed) {
                verticalVelocity *= 1.07f;
                lastHoldingJumpBoost = Time.time;
            }
        }

        // Move the character
<<<<<<<< HEAD:Assets/Scripts/Alex/PlayerMovement.cs
        playerAnimator.SetBool("Run", horizontalVelocity != 0);
        playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        if (stopMovement) playerRigidbody.velocity = Vector2.zero;
========
        playerRigidbody.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);
>>>>>>>> water:OneDrive/Desktop/UnityP1/Assets/Scripts/PlayerMovement.cs
    }

    private void CheckEnvironment() {
        Vector2 playerPos = (Vector2)transform.position;
        Vector2 playerFeet = playerPos - new Vector2(0, playerCollider.bounds.extents.y);
        Vector2 playerHead = playerPos + new Vector2(0, playerCollider.bounds.extents.y);
        Vector2 playerBox = new(playerCollider.bounds.size.x, playerCollider.bounds.size.y);

        isGrounded = Physics2D.Raycast(playerFeet, Vector2.down, 0.1f, groundLayer) && playerRigidbody.linearVelocity.y == 0;
        isTouchingWater = Physics2D.BoxCast(playerPos, playerBox, 0, Vector2.zero, 0f, waterLayer);
        isSwimming = Physics2D.Raycast(playerPos, Vector2.up, 0.1f, waterLayer);

    }

    public bool IsUnderwater() {
        return isSwimming;
    }

    public void ToggleFlight() {
        canFly = !canFly;

        Physics2D.gravity = canFly ? Vector3.zero : new Vector3(0f, -9.81f, 0f);
    }

    public void ToggleMovement() {
        stopMovement = !stopMovement;
    }
}

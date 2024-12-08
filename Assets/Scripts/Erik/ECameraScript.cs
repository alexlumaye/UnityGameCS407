using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECameraScript : MonoBehaviour
{
    public Transform player;        // The player's transform
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement
    public float yOffset = 2f;      // Offset to keep the player at a comfortable position in the view
    public float upwardSpeed = 0.5f;

    private Vector3 targetPosition; // Target position for the camera

    void LateUpdate() {
        if (player != null) {
            // Determine the target position
            targetPosition = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);

            // Smoothly interpolate the camera's position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            transform.position += Vector3.up * upwardSpeed * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoundaries : MonoBehaviour
{
    public float minX = -10f; 
    public float maxX = 10f;  
    public float minY = -5f;  
    public float maxY = 5f;   
    void Update()
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

        transform.position = currentPosition;
    }
}

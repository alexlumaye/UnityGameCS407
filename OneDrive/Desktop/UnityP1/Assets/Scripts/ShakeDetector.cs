
using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f;
    private Vector3 lastAcceleration;

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        float deltaAcceleration = (acceleration - lastAcceleration).magnitude;

        if (deltaAcceleration > shakeThreshold)
        {
            SpawnAirBubble();
        }

        lastAcceleration = acceleration;
    }

    void SpawnAirBubble()
    {
        Debug.Log("Phone shaken! Air bubble created!");
        Vector2 randomPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Instantiate(Resources.Load("AirBubble"), randomPosition, Quaternion.identity);
    }
}


using UnityEngine;

public class Hazard : MonoBehaviour
{
    private Vector2 swipeStart;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeEnd = touch.position;
                Vector2 swipeDirection = swipeEnd - swipeStart;

                if (swipeDirection.magnitude > 100f)
                {
                    AvoidHazard(swipeDirection.normalized);
                }
            }
        }
    }

    void AvoidHazard(Vector2 direction)
    {
        Debug.Log($"Swiped! Avoiding hazard in direction: {direction}");
    }
}

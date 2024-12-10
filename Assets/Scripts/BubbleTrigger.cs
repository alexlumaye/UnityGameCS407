using UnityEngine;

public class BubbleTrigger : MonoBehaviour
{
    public GameObject wings;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WingTrigger"))
        {
            wings.SetActive(true);
        }
    }
}

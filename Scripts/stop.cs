using UnityEngine;
using UnityEngine.SceneManagement; 

public class stop : MonoBehaviour
{
    public Animator animator;  

    void OnTriggerEnter2D(Collider2D item)
    {
        if (item.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation");  
                
            }
            else
            {
                Debug.LogError("Animator not assigned in the inspector.");
            }
        }
    }
}

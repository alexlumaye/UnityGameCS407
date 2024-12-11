using UnityEngine;
using UnityEngine.SceneManagement; 
public class CombineManager : MonoBehaviour
{
    private int totalItemsCollected = 0; // Track total items collected

    public void AddItem(string itemName)
    {
        totalItemsCollected++; // Increment total item count
        Debug.Log($"Total items collected: {totalItemsCollected}");

        // Check if the total items collected reaches 6
        if (totalItemsCollected >= 6)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("You collected 6 items. The game is over!");

        SceneManager.LoadScene("TransitionScene");
    }
}

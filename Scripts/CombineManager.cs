using UnityEngine;
using UnityEngine.SceneManagement; // For scene management to restart or quit the game

public class CombineManager : MonoBehaviour
{
    private int totalItemsCollected = 0; // Track total items collected

    public void AddItem(string itemName)
    {
        totalItemsCollected++; // Increment total item count
        Debug.Log($"Total items collected: {totalItemsCollected}");

        // Check if the total items collected reaches 3
        if (totalItemsCollected >= 6)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("You collected 6 items. The game is over!");
        
        // Optionally, load a specific "Game Over" or "Win" scene
        // Replace "GameOverScene" with the actual name of your scene
        SceneManager.LoadScene("TransitionScene");

        // Or quit the application (useful for a build)
        // Application.Quit();
    }
}

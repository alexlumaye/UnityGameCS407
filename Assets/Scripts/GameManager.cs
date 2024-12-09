
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Game restarted!");
        // Add logic to restart the game
    }

    public void QuitGame()
    {
        Debug.Log("Game exited!");
        Application.Quit();
    }
}

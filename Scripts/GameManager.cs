
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Game restarted!");
    }

    public void QuitGame()
    {
        Debug.Log("Game exited!");
        Application.Quit();
    }
}

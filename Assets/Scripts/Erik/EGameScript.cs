using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGameScript : MonoBehaviour
{

    public EPlayerScript playerScript;
    public GameObject endGameUI;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.health <= 0) {
            EndGame();
        }
    }

    void EndGame() {
        Time.timeScale = 0;
        endGameUI.SetActive(true);
    }
}

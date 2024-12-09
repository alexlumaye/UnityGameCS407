using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGameScript : MonoBehaviour
{

    public EPlayerScript playerScript;
    public GameObject endGameUI;
    bool started = false;
    public GameObject instructionPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        instructionPanel.SetActive(true);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.health <= 0) {
            EndGame();
        }
        if (!started) {
            if (Input.GetKey(KeyCode.Space)) { StartGame(); }
            if (!started && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {  StartGame(); }
        }
    }

    void StartGame() {
        instructionPanel.SetActive(false);
        Time.timeScale = 1;
        started = true;
    }

    void EndGame() {
        Time.timeScale = 0;
        endGameUI.SetActive(true);
    }
}

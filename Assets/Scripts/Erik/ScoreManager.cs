using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public EPlayerScript playerScript;
    [SerializeField]
    public TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore() {
        submitScoreEvent.Invoke(inputName.text, playerScript.score);
    }
        
}

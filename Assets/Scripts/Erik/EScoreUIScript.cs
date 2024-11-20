using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EScoreUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public EPlayerScript playerScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerScript.score.ToString();
    }
}

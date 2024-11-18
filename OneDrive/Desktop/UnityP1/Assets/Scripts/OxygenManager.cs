
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public Slider oxygenBar;
    public float oxygenDepletionRate = 0.5f;
    public float oxygenIncrease = 20f;

    void Update()
    {
        oxygenBar.value -= oxygenDepletionRate * Time.deltaTime;
        if (oxygenBar.value <= 0)
        {
            GameOver();
        }
    }

    public void ReplenishOxygen()
    {
        oxygenBar.value = Mathf.Min(oxygenBar.value + oxygenIncrease, oxygenBar.maxValue);
    }

    void GameOver()
    {
        Debug.Log("You drowned!");
    }
}
